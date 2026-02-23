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
        // 1. Fetch Usuario
        var usuario = await _context.Usuarios.FindAsync(notaRequestDto.UsuarioId);
        if (usuario == null)
            throw new Exception("Usuario no encontrado");

        // 2. Fetch ClienteProveedor
        var clienteProveedor = await _clienteProveedorRepository.GetByIdAsync(notaRequestDto.ClienteProveedorId);
        if (clienteProveedor == null)
            throw new Exception("Cliente/Proveedor no encontrado");

        // 3. Create Nota
        var nota = new Nota
        {
            Fecha = DateTime.UtcNow,
            TipoNota = notaRequestDto.Tipo,
            ClienteProveedor = clienteProveedor,
            Usuario = usuario,
            Total = notaRequestDto.Total,
            Observaciones = notaRequestDto.Observaciones,
            Impuestos = notaRequestDto.Impuestos,
            Descuentos = notaRequestDto.Descuentos,
            Estado = notaRequestDto.Estado
        };

        await _notaRepository.CreateNota(nota);

        // 4. Create Movimientos and Validate
        var movimientosCreated = new List<Movimiento>();
        foreach (var movimientoDto in notaRequestDto.Movimientos)
        {
            movimientoDto.TipoMovimiento = nota.TipoNota == "Compra" ? "Entrada" : "Salida";

            // Validating that the product and almacenes are valid
            var producto = await _context.Productos.FindAsync(movimientoDto.ProductoId);
            if(producto == null) throw new Exception("Producto no encontrado");

            var almacen = await _context.Almacenes.FindAsync(movimientoDto.AlmacenId);
            if(almacen == null) throw new Exception("Almacen no encontrado");

            var movimientoToCreate = new Movimiento
            {
                Nota = nota,
                Producto = producto,
                Almacen = almacen,
                Cantidad = movimientoDto.Cantidad,
                PrecioUnitarioCompra = movimientoDto.PrecioUnitarioCompra,
                PrecioUnitarioVenta = movimientoDto.PrecioUnitarioVenta,
                TipoMovimiento = movimientoDto.TipoMovimiento,
                Observaciones = movimientoDto.Observaciones
            };

            await _movimientoRepository.CreateMovimiento(movimientoToCreate);
            movimientosCreated.Add(movimientoToCreate);
        }

        // 5. Update Stock
        foreach (var movimiento in movimientosCreated)
        {
            var almacenProductoRetrieved = await _almacenProductoRepository.GetByAlmacenAndProductoAsync(movimiento.Almacen.Id, movimiento.Producto.Id);
            
            if (almacenProductoRetrieved == null)
                throw new Exception("AlmacenProducto no encontrado");

            switch (movimiento.TipoMovimiento)
            {
                case "Entrada":
                    almacenProductoRetrieved.CantidadActual += (int)movimiento.Cantidad;
                    break;
                case "Salida":
                    almacenProductoRetrieved.CantidadActual -= (int)movimiento.Cantidad;
                    if (almacenProductoRetrieved.CantidadActual < 0) 
                        throw new Exception("Stock insuficiente");
                    break;
                default:
                    break;
            }

            await _almacenProductoRepository.UpdateAsync(almacenProductoRetrieved);
        }

        return new NotaDto
        {
            Id = nota.Id,
            Fecha = nota.Fecha,
            TipoNota = nota.TipoNota,
            Total = nota.Total,
            Impuestos = nota.Impuestos,
            Descuentos = nota.Descuentos,
            Estado = nota.Estado,
            Observaciones = nota.Observaciones
        };
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

                                table.Cell().Element(CellStyle).Text(item.Producto?.Nombre ?? "Sin nombre");
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
