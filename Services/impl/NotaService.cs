using System;
using ComprasVentas.Data;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.spec;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ComprasVentas.Services.impl;

public class NotaService : INotaService
{
    private readonly ClienteProveedorRepository _clienteProveedorRepository;

    private readonly MovimientoRepository _movimientoRepository;

    private readonly NotaRepository _notaRepository;

    private readonly AlmacenProductoRepository _almacenProductoRepository;

    private readonly AppDbContext _context;

    public NotaService(ClienteProveedorRepository clienteProveedorRepository,
        MovimientoRepository movimientoRepository, NotaRepository notaRepository,
        AlmacenProductoRepository almacenProductoRepository, AppDbContext context)
    {
        _clienteProveedorRepository = clienteProveedorRepository;
        _movimientoRepository = movimientoRepository;
        _notaRepository = notaRepository;
        _almacenProductoRepository = almacenProductoRepository;
        _context = context;
    }

    public async Task<NotaDto> CreateNotaASync(CreateNotaDto notaRequestDto)
    {
        // Create Nota
        var nota = new Nota
        {
            Fecha = DateTime.Now,
            TipoNota = notaRequestDto.Tipo,
            ClienteProveedor = await _clienteProveedorRepository.GetByIdAsync(notaRequestDto.ClienteProveedorId),
            Total = notaRequestDto.Total,
            Observaciones = notaRequestDto.Observaciones,
            Impuestos = notaRequestDto.Impuestos,
            Descuentos = notaRequestDto.Descuentos,
            Estado = notaRequestDto.Estado
        };
        await _notaRepository.CreateNota(nota);

        // Create Movimientos & Validate Stock
        foreach (var movimientoDto in notaRequestDto.Movimientos)
        {
            var almacenProducto = await _almacenProductoRepository.GetByAlmacenAndProductoAsync(movimientoDto.AlmacenId, movimientoDto.ProductoId);

            if (almacenProducto == null)
            {
                throw new Exception($"Producto {movimientoDto.ProductoId} no encontrado en almacen {movimientoDto.AlmacenId}");
            }

            // Validate stock if it's a salida
            if (notaRequestDto.Tipo == "Salida")
            {
                if (almacenProducto.CantidadActual < movimientoDto.Cantidad)
                    throw new Exception($"Stock insuficiente para producto {almacenProducto.Producto?.Nombre ?? "Desconocido"} en almacen");

                almacenProducto.CantidadActual -= (int)movimientoDto.Cantidad;
            }
            else if (notaRequestDto.Tipo == "Entrada")
            {
                almacenProducto.CantidadActual += (int)movimientoDto.Cantidad;
            }

            await _almacenProductoRepository.UpdateAsync(almacenProducto);

            var movimiento = new Movimiento
            {
                Nota = nota,
                Producto = _context.Productos.Find(movimientoDto.ProductoId),
                Cantidad = movimientoDto.Cantidad,
                PrecioUnitarioCompra = movimientoDto.PrecioUnitarioCompra,
                PrecioUnitarioVenta = movimientoDto.PrecioUnitarioVenta,
                TipoMovimiento = movimientoDto.TipoMovimiento,
                Observaciones = movimientoDto.Observaciones,
                Almacen = almacenProducto.Almacen
            };
            await _movimientoRepository.CreateMovimiento(movimiento);
        }

        var notaDto = new NotaDto
        {
            Id = nota.Id,
            Fecha = nota.Fecha,
            TipoNota = nota.TipoNota,
            //ClienteProveedorId = nota.ClienteProveedor != null ? nota.ClienteProveedor.Id : 0, 
            Total = nota.Total,
            Impuestos = nota.Impuestos,
            Descuentos = nota.Descuentos,
            Estado = nota.Estado,
            Observaciones = nota.Observaciones
        };

        return notaDto;

    }

    public async Task<byte[]> GenerateNotaReportPdfAsync(int notaId)
    {
        var nota = await _notaRepository.GetByIdAsync(notaId);
        if (nota == null)
        {
            throw new Exception($"Nota con ID {notaId} no encontrada");
        }
        QuestPDF.Settings.License = LicenseType.Community;
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.Letter);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));
                page.Header()
                    .Row(row =>
                    {
                        row.RelativeItem().Column(column =>
                        {
                            column.Item().Text($"Nota - {nota.Id}").SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);
                            column.Item().Text($"Fecha: {nota.Fecha:dd/MM/yyyy}");
                            column.Item().Text($"Cliente/Proveedor: {nota.ClienteProveedor?.RazonSocial ?? "Desconocido"}");
                        });
                        row.ConstantItem(100).Height(50).Placeholder("Logo");
                    });
                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(column =>
                    {
                        column.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn(3);
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });
                            table.Header(header =>
                           {
                                 header.Cell().Element(CellStyle).Text("Producto");
                                 header.Cell().Element(CellStyle).Text("Cantidad");
                                 header.Cell().Element(CellStyle).Text("Precio");
                                 header.Cell().Element(CellStyle).Text("Total");
                                 static IContainer CellStyle(IContainer container)
                                 {
                                     return container.DefaultTextStyle(x => x.SemiBold()).Padding(5).Border(1).BorderColor(Colors.Grey.Lighten2);
                                 }
                             });
                            foreach (var item in nota.Movimientos)
                            {
                                var precio = nota.TipoNota == "Entrada" ? item.PrecioUnitarioCompra : item.PrecioUnitarioVenta;
                                var totalItem = item.Cantidad * precio;

                                table.Cell().Element(CellStyle).Text(item.Producto.Nombre ?? "Sin nombre");
                                table.Cell().Element(CellStyle).Text(item.Cantidad.ToString() ?? "0");
                                table.Cell().Element(CellStyle).Text($"{precio:F2}" ?? "0");
                                table.Cell().Element(CellStyle).Text($"{totalItem:F2}" ?? "0");
                                static IContainer CellStyle(IContainer container)
                                {
                                    return container.BorderBottom(1).BorderColor(Colors.Grey.Lighten2).PaddingVertical(5);
                                }

                            }
                        });
                        column.Item().AlignRight().Text($"Subtotal: {(nota.Total - nota.Impuestos + nota.Descuentos):F2}").SemiBold().FontSize(14);

                    });
                page.Footer()
                    .AlignCenter()
                    .Text(x=>
                    {
                        x.Span("Gracias por su compra!").FontSize(12);
                        x.Line("www.comprasventas.com").FontSize(10).FontColor(Colors.Grey.Medium);
                    });
            });
        });
        return document.GeneratePdf();
    }

    public async Task<List<NotaDto>> GetAllNotasAsync()
    {
        var notas = await _notaRepository.GetAllAsync();
        return notas.Select(n => new NotaDto
        {
            Id = n.Id,
            Fecha = n.Fecha,
            TipoNota = n.TipoNota,
            Total = n.Total,
            Impuestos = n.Impuestos,
            Descuentos = n.Descuentos,
            Estado = n.Estado,
            Observaciones = n.Observaciones
        }).ToList();
    }
}
