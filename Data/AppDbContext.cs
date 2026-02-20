using System;
using ComprasVentas.Models;
using Microsoft.EntityFrameworkCore;

namespace ComprasVentas.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Rol> Roles { get; set;}

    public DbSet<Permiso> Permisos { get; set; }

    public DbSet<Usuario> Usuarios { get; set; }

    public DbSet<Persona> Personas { get; set; }

    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public DbSet<Sucursal> Sucursales { get; set; }

    public DbSet<SucursalUser> SucursalUsers { get; set; }

    public DbSet<Almacen> Almacenes { get; set; }

    public DbSet<Categoria> Categorias { get; set; }

    public DbSet<Producto> Productos { get; set; }

    public DbSet<AlmacenProducto> AlmacenProductos { get; set; }

    public DbSet<Nota> Notas { get; set; }

    public DbSet<Movimiento> Movimientos { get; set; }

    public DbSet<ClienteProveedor> ClienteProveedor { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>()
            .HasMany(r=>r.Permisos)
            .WithMany(p=>p.Roles)
            .UsingEntity<Dictionary<string, object>>(
        "permiso_rol",
        j => j.HasOne<Permiso>().WithMany().HasForeignKey("PermisosId"),
        j => j.HasOne<Rol>().WithMany().HasForeignKey("RolesId"),
        j =>
        {
            j.HasKey("PermisosId", "RolesId");
            j.ToTable("permiso_rol");
        }
    );

        modelBuilder.Entity<Usuario>()
            .HasMany(r=>r.Roles)
            .WithMany(p=>p.Usuarios)
            .UsingEntity<Dictionary<string, object>>(
        "usuario_rol",
        j => j.HasOne<Rol>().WithMany().HasForeignKey("RolesId"),
        j => j.HasOne<Usuario>().WithMany().HasForeignKey("UsuariosId"),
        j =>
        {
            j.HasKey("UsuariosId", "RolesId");
            j.ToTable("usuario_rol");
        }
    );
   
        modelBuilder.Entity<Usuario>()
            .HasOne(u=>u.Persona)
            .WithOne(p=>p.Usuario)
            .HasForeignKey<Persona>(p=>p.Id); 

        modelBuilder.Entity<Usuario>(e=>
        {
            e.Property(u=>u.Nombre).IsRequired().HasMaxLength(50);
            e.Property(u=>u.Correo).IsRequired().HasMaxLength(255);
            e.Property(u=>u.Password).IsRequired().HasMaxLength(255);
        });

        modelBuilder.Entity<Persona>(e=>
        {
            e.Property(p=>p.Nombres).IsRequired().HasMaxLength(100);
            e.Property(p=>p.Apellidos).IsRequired().HasMaxLength(100);
            e.Property(p=>p.Genero).HasMaxLength(20);
            e.Property(p=>p.Telefono).HasMaxLength(20);
            e.Property(p=>p.Direccion).HasMaxLength(255);
            e.Property(p=>p.Nacionalidad).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Permiso>(e=>
        {
            e.Property(u=>u.Nombre).IsRequired().HasMaxLength(100);
            e.Property(u=>u.Recurso).HasMaxLength(100);
            e.Property(u=>u.Accion).IsRequired().HasMaxLength(100);
        });

        modelBuilder.Entity<Rol>(e=>
        {
            e.Property(u=>u.Nombre).IsRequired().HasMaxLength(100);
            e.Property(u=>u.Descripcion).HasMaxLength(255);
        });

        modelBuilder.Entity<RefreshToken>(e=>
        {
            e.Property(r=>r.Token).IsRequired().HasMaxLength(500);
            e.HasOne(r=>r.Usuario)
             .WithMany(u=>u.RefreshTokens)
             .HasForeignKey(r=>r.UsuarioId);
        });

        // Configurations for new entities
        modelBuilder.Entity<Sucursal>(e=>
        {
            e.Property(s=>s.Nombre).IsRequired().HasMaxLength(255);
            e.Property(s=>s.Direccion).IsRequired();
            e.Property(s=>s.Telefono).IsRequired().HasMaxLength(20);
            e.Property(s=>s.Ciudad).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<SucursalUser>(e=>
        {
            e.HasOne(su=>su.Sucursal)
             .WithMany(s=>s.SucursalUsers)
             .HasForeignKey("SucursalId");
            e.HasOne(su=>su.Usuario)
             .WithMany(u=>u.SucursalUsers)
             .HasForeignKey("UsuarioId");
        });

        modelBuilder.Entity<Almacen>(e=>
        {
            e.Property(a=>a.Nombre).IsRequired().HasMaxLength(100);
            e.Property(a=>a.Codigo).HasMaxLength(20);
            e.Property(a=>a.Direccion).IsRequired();
            e.Property(a=>a.Telefono).IsRequired().HasMaxLength(20);
            e.Property(a=>a.Ciudad).IsRequired().HasMaxLength(50);
            e.HasOne(a=>a.Sucursal)
             .WithMany(s=>s.Almacenes)
             .HasForeignKey("SucursalId");
        });

        modelBuilder.Entity<Categoria>(e=>
        {
            e.Property(c=>c.Nombre).IsRequired().HasMaxLength(50);
        });

        modelBuilder.Entity<Producto>(e=>
        {
            e.Property(p=>p.Nombre).IsRequired().HasMaxLength(200);
            e.Property(p=>p.UnidadMedida).HasMaxLength(20);
            e.Property(p=>p.Marca).HasMaxLength(100);
            e.Property(p=>p.PrecioVentaActual).HasPrecision(12, 2);
            e.Property(p=>p.Imagen).HasMaxLength(255);
            e.HasOne(p=>p.Categoria)
             .WithMany(c=>c.Productos)
             .HasForeignKey("CategoriaId");
        });

        modelBuilder.Entity<AlmacenProducto>(e=>
        {
            e.HasOne(ap=>ap.Producto)
             .WithMany(p=>p.AlmacenProductos)
             .HasForeignKey("ProductoId");
            e.HasOne(ap=>ap.Almacen)
             .WithMany(a=>a.AlmacenProductos)
             .HasForeignKey("AlmacenId");
        });

        modelBuilder.Entity<Nota>(e=>
        {
            e.Property(n=>n.TipoNota).IsRequired().HasMaxLength(30);
            e.Property(n=>n.Impuestos).HasPrecision(13,2);
            e.Property(n=>n.Total).HasPrecision(13, 2);
            e.Property(n=>n.Descuentos).HasPrecision(13, 2);
            e.Property(n=>n.Observaciones).HasMaxLength(500);
            e.HasOne(n=>n.Usuario)
             .WithMany(u=>u.Notas)
             .HasForeignKey("UsuarioId");
            e.HasOne(n=>n.ClienteProveedor)
             .WithMany(cp=>cp.Notas)
             .HasForeignKey("ClienteProveedorId");
        });

        modelBuilder.Entity<Movimiento>(e=>
        {
            e.Property(m=>m.TipoMovimiento).IsRequired().HasMaxLength(30);
            e.Property(m=>m.PrecioUnitarioCompra).HasPrecision(13, 2);
            e.Property(m=>m.PrecioUnitarioVenta).HasPrecision(13, 2);
            e.Property(m=>m.Observaciones).HasMaxLength(500);
            e.HasOne(m=>m.Producto)
             .WithMany(p=>p.Movimientos)
             .HasForeignKey("ProductoId");
            e.HasOne(m=>m.Almacen)
             .WithMany(a=>a.Movimientos)
             .HasForeignKey("AlmacenId");
            e.HasOne(m=>m.Nota)
             .WithMany(n=>n.Movimientos)
             .HasForeignKey("NotaId");
        });
        modelBuilder.Entity<ClienteProveedor>(e=>
        {
            e.Property(cp=>cp.Estado).IsRequired();
            e.Property(cp=>cp.Tipo).IsRequired().HasMaxLength(20);
            e.Property(cp=>cp.Telefono).HasMaxLength(20);
            e.Property(cp=>cp.NroIdentificacion).HasMaxLength(100);
            e.Property(cp=>cp.RazonSocial).HasMaxLength(200);
            e.Property(cp=>cp.Direccion).HasMaxLength(255);
            e.Property(cp=>cp.Correo).HasMaxLength(255);
        });
    }
}
