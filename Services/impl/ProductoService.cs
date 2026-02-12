using System;
using ComprasVentas.Data;
using ComprasVentas.Dto;
using ComprasVentas.Dto.common;
using ComprasVentas.Models;
using ComprasVentas.Services.spec;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Services.impl;

public class ProductoService : IProductoService
{

    private readonly AppDbContext _context;

    private readonly IFileService _fileService;

    public ProductoService(AppDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }
    
    public async Task<PageResultDto<ProductoDto>> GetProductosAsync(ProductoFilterDto productoFilterDto)
    {
        var query = _context.Productos.Include(p=>p.Categoria).AsQueryable();

        //global filter
        if(!string.IsNullOrEmpty(productoFilterDto.filterValue))
        {
            var filterValue = productoFilterDto.filterValue.ToLower();
            query = query.Where(p=>
                p.Nombre.ToLower().Contains(filterValue) ||
                p.Descripcion.ToLower().Contains(filterValue) ||
                p.Marca.ToLower().Contains(filterValue) ||
                p.Categoria.Nombre.ToLower().Contains(filterValue)
            );
        }
        //specific filters
        if(!string.IsNullOrEmpty(productoFilterDto.Nombre))
           query = query.Where(p=>p.Nombre.ToLower().Contains(productoFilterDto.Nombre.ToLower()));

        if(!string.IsNullOrEmpty(productoFilterDto.Descripcion))
           query = query.Where(p=>p.Descripcion.ToLower().Contains(productoFilterDto.Descripcion.ToLower()));
        
        if(!string.IsNullOrEmpty(productoFilterDto.Marca))
           query = query.Where(p=>p.Marca.ToLower().Contains(productoFilterDto.Marca.ToLower()));
        
        if(!string.IsNullOrEmpty(productoFilterDto.NombreCategoria))
           query = query.Where(p=>p.Categoria != null && p.Categoria.Nombre.ToLower().Contains(productoFilterDto.NombreCategoria.ToLower()));

        //Sorting
        if(!string.IsNullOrEmpty(productoFilterDto.SortField))
        {
            var sortField = productoFilterDto.SortField.ToLower();
            var sortOrder = productoFilterDto.SortOrder.ToLower() == "desc" ? "descending" : "ascending";
            query = ApplySorting(query, productoFilterDto.SortField, productoFilterDto.SortOrder);
        }
        else
        {
            query = query.OrderBy(p=>p.Id);
        }

        //Pagination
        var TotalCount = await query.CountAsync();

        var items = await query.Skip((productoFilterDto.Page - 1) * productoFilterDto.Size)
                               .Take(productoFilterDto.Size)
                               .Select(p=> new ProductoDto
                               {
                                   Id = p.Id,
                                   Nombre = p.Nombre,
                                   Descripcion = p.Descripcion,
                                   UnidadMedida = p.UnidadMedida,
                                   Marca = p.Marca,
                                   Precio = p.PrecioVentaActual,
                                   Imagen = p.Imagen,
                                   Estado = p.Estado,
                                   CategoriaId = p.Categoria != null ? p.Categoria.Id : 0
                               }).ToListAsync();  
    
        return new PageResultDto<ProductoDto>
        {
            Items = items,
            TotalCount = TotalCount,
            Page = productoFilterDto.Page,
            Size = productoFilterDto.Size
        };
    }


    public async Task<ProductoDto> CreateAsync(CreateProductoDto createProductoDto)
    {
        var producto = new Producto
        {
            Nombre = createProductoDto.Nombre,
            Descripcion = createProductoDto.Descripcion,
            PrecioVentaActual = createProductoDto.Precio,
            Estado = createProductoDto.Estado,
            Categoria = _context.Categorias.Find(createProductoDto.CategoriaId)
        };

        //add File to producto
        if(createProductoDto.Imagen != null)
        {
            var imagePath = await _fileService.SaveFileAsync(createProductoDto.Imagen);
            producto.Imagen = imagePath;
        } 

        if(string.IsNullOrEmpty(producto.Imagen))
        {
            throw new Exception("Error al guardar la imagen del producto");
        }

        _context.Productos.Add(producto);
        await _context.SaveChangesAsync();

        var createdProducto = await _context.Productos.Include(p=>p.Categoria).FirstOrDefaultAsync(p=>p.Id == producto.Id);

        return new ProductoDto
        {
            Id = createdProducto.Id,
            Nombre = createdProducto.Nombre,
            Descripcion = createdProducto.Descripcion,
            UnidadMedida = createdProducto.UnidadMedida,
            Marca = createdProducto.Marca,
            Precio = createdProducto.PrecioVentaActual,
            Imagen = createdProducto.Imagen,
            Estado = createdProducto.Estado,
            CategoriaId = createdProducto.Categoria != null ? createdProducto.Categoria.Id : 0
        };
    }

    private IQueryable<Producto> ApplySorting(IQueryable<Producto> query, string sortField, string sortOrder)
    {
        bool isDesc = sortOrder == "desc";

        if(sortField.Equals("NombreCategoria", StringComparison.OrdinalIgnoreCase))
             return isDesc ? query.OrderByDescending(p=>p.Categoria != null ? p.Categoria.Nombre : null) 
                           : query.OrderBy(p=>p.Categoria != null ? p.Categoria.Nombre : null);

        
        var property  = typeof(Producto).GetProperty(sortField, System.Reflection.BindingFlags.IgnoreCase | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
    
        if(property != null)
        {
            switch (sortField)
            {
                case "nombre":
                    return isDesc ? query.OrderByDescending(p=>EF.Property<string>(p, property.Name)) 
                                  : query.OrderBy(p=>EF.Property<string>(p, property.Name));
                case "descripcion":
                    return isDesc ? query.OrderByDescending(p=>EF.Property<decimal>(p, property.Name)) 
                                  : query.OrderBy(p=>EF.Property<decimal>(p, property.Name));
                case "marca":
                    return isDesc ? query.OrderByDescending(p=>EF.Property<int>(p, property.Name)) 
                                  : query.OrderBy(p=>EF.Property<int>(p, property.Name));
                case "id":
                    return isDesc ? query.OrderByDescending(p=>EF.Property<bool>(p, property.Name)) 
                                  : query.OrderBy(p=>EF.Property<bool>(p, property.Name));
                default:
                    return isDesc ? query.OrderByDescending(p=>EF.Property<bool>(p, property.Name)) 
                                  : query.OrderBy(p=>EF.Property<bool>(p, property.Name));
            }
        }
        return query;
            
    }

    public async Task<List<ProductoDto>> FindAllProductosByAlmacenAsync(int almacenId)
    {
        var productos = await _context.AlmacenProductos
            .Where(ap => ap.Almacen.Id == almacenId)
            .Include(ap => ap.Producto)
            .ThenInclude(p => p.Categoria)
            .Select(ap => ap.Producto)
            .ToListAsync();

        return productos.Select(p => new ProductoDto
        {
            Id = p.Id,
            Nombre = p.Nombre,
            Descripcion = p.Descripcion,
            UnidadMedida = p.UnidadMedida,
            Marca = p.Marca,
            Precio = p.PrecioVentaActual,
            Imagen = p.Imagen,
            Estado = p.Estado,
            CategoriaId = p.Categoria != null ? p.Categoria.Id : 0
        }).ToList();
    }

}
