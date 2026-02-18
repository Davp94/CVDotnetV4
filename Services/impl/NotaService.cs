using System;
using ComprasVentas.Dto;
using ComprasVentas.Models;
using ComprasVentas.Repository;
using ComprasVentas.Services.spec;

namespace ComprasVentas.Services.impl;

public class NotaService : INotaService
{
    private readonly ClienteProveedorRepository _clienteProveedorRepository;

    private readonly MovimientoRepository _movimientoRepository;

    private readonly NotaRepository _notaRepository;

    private readonly AlmacenProductoRepository _almacenProductoRepository;

    public NotaService(ClienteProveedorRepository clienteProveedorRepository,
        MovimientoRepository movimientoRepository, NotaRepository notaRepository,
        AlmacenProductoRepository almacenProductoRepository)
    {
        _clienteProveedorRepository = clienteProveedorRepository;
        _movimientoRepository = movimientoRepository;
        _notaRepository = notaRepository;
        _almacenProductoRepository = almacenProductoRepository;
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

            var movimiento = new Models.Movimiento
            {
                NotaId = nota.Id,
                ProductoId = movimientoDto.ProductoId,
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
