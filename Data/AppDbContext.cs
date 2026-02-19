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
        //add data seeding
        SeedData(modelBuilder);
    }

      private void SeedData(ModelBuilder modelBuilder)
    {
        // ========== PERMISOS ==========
        // Permisos para ROLES
        var permisoRolCreate = new Permiso { Id = 1, Nombre = "ROL_CREATE", Recurso = "ROL", Accion = "CREATE" };
        var permisoRolRead = new Permiso { Id = 2, Nombre = "ROL_READ", Recurso = "ROL", Accion = "READ" };
        var permisoRolUpdate = new Permiso { Id = 3, Nombre = "ROL_UPDATE", Recurso = "ROL", Accion = "UPDATE" };
        var permisoRolDelete = new Permiso { Id = 4, Nombre = "ROL_DELETE", Recurso = "ROL", Accion = "DELETE" };

        // Permisos para USUARIOS
        var permisoUsuarioCreate = new Permiso { Id = 5, Nombre = "USUARIO_CREATE", Recurso = "USUARIO", Accion = "CREATE" };
        var permisoUsuarioRead = new Permiso { Id = 6, Nombre = "USUARIO_READ", Recurso = "USUARIO", Accion = "READ" };
        var permisoUsuarioUpdate = new Permiso { Id = 7, Nombre = "USUARIO_UPDATE", Recurso = "USUARIO", Accion = "UPDATE" };
        var permisoUsuarioDelete = new Permiso { Id = 8, Nombre = "USUARIO_DELETE", Recurso = "USUARIO", Accion = "DELETE" };

        // Permisos para PERSONAS
        var permisoPersonaCreate = new Permiso { Id = 9, Nombre = "PERSONA_CREATE", Recurso = "PERSONA", Accion = "CREATE" };
        var permisoPersonaRead = new Permiso { Id = 10, Nombre = "PERSONA_READ", Recurso = "PERSONA", Accion = "READ" };
        var permisoPersonaUpdate = new Permiso { Id = 11, Nombre = "PERSONA_UPDATE", Recurso = "PERSONA", Accion = "UPDATE" };
        var permisoPersonaDelete = new Permiso { Id = 12, Nombre = "PERSONA_DELETE", Recurso = "PERSONA", Accion = "DELETE" };

        // Permisos para PERMISOS
        var permisoPermisoCreate = new Permiso { Id = 13, Nombre = "PERMISO_CREATE", Recurso = "PERMISO", Accion = "CREATE" };
        var permisoPermisoRead = new Permiso { Id = 14, Nombre = "PERMISO_READ", Recurso = "PERMISO", Accion = "READ" };
        var permisoPermisoUpdate = new Permiso { Id = 15, Nombre = "PERMISO_UPDATE", Recurso = "PERMISO", Accion = "UPDATE" };
        var permisoPermisoDelete = new Permiso { Id = 16, Nombre = "PERMISO_DELETE", Recurso = "PERMISO", Accion = "DELETE" };

        // Permisos para DASHBOARD
        var permisoDashboardRead = new Permiso { Id = 17, Nombre = "DASHBOARD_READ", Recurso = "DASHBOARD", Accion = "READ" };

        modelBuilder.Entity<Permiso>().HasData(
            permisoRolCreate, permisoRolRead, permisoRolUpdate, permisoRolDelete,
            permisoUsuarioCreate, permisoUsuarioRead, permisoUsuarioUpdate, permisoUsuarioDelete,
            permisoPersonaCreate, permisoPersonaRead, permisoPersonaUpdate, permisoPersonaDelete,
            permisoPermisoCreate, permisoPermisoRead, permisoPermisoUpdate, permisoPermisoDelete,
            permisoDashboardRead
        );

        // ========== ROLES ==========
        var rolSuperAdmin = new Rol
        {
            Id = 1,
            Nombre = "Super Administrador",
            Descripcion = "Acceso completo a todos los recursos del sistema"
        };

        var rolAdministrador = new Rol
        {
            Id = 2,
            Nombre = "Administrador",
            Descripcion = "Administración del sistema (sin permisos de super administrador)"
        };

        var rolUsuario = new Rol
        {
            Id = 3,
            Nombre = "Usuario",
            Descripcion = "Usuario regular del sistema con permisos limitados"
        };

        var rolInvitado = new Rol
        {
            Id = 4,
            Nombre = "Invitado",
            Descripcion = "Usuario con acceso de solo lectura"
        };

        modelBuilder.Entity<Rol>().HasData(rolSuperAdmin, rolAdministrador, rolUsuario, rolInvitado);

        // ========== RELACION ROL-PERMISO ==========
        // Configurar la relación many-to-many para los roles y permisos
        modelBuilder.Entity("permiso_rol").HasData(
            // Super Administrador - Todos los permisos
            new { PermisosId = 1, RolesId = 1 },
            new { PermisosId = 2, RolesId = 1 },
            new { PermisosId = 3, RolesId = 1 },
            new { PermisosId = 4, RolesId = 1 },
            new { PermisosId = 5, RolesId = 1 },
            new { PermisosId = 6, RolesId = 1 },
            new { PermisosId = 7, RolesId = 1 },
            new { PermisosId = 8, RolesId = 1 },
            new { PermisosId = 9, RolesId = 1 },
            new { PermisosId = 10, RolesId = 1 },
            new { PermisosId = 11, RolesId = 1 },
            new { PermisosId = 12, RolesId = 1 },
            new { PermisosId = 13, RolesId = 1 },
            new { PermisosId = 14, RolesId = 1 },
            new { PermisosId = 15, RolesId = 1 },
            new { PermisosId = 16, RolesId = 1 },
            new { PermisosId = 17, RolesId = 1 },

            // Administrador - Permisos de gestión (sin permisos de PERMISO)
            new { PermisosId = 1, RolesId = 2 }, // ROL_CREATE
            new { PermisosId = 2, RolesId = 2 }, // ROL_READ
            new { PermisosId = 3, RolesId = 2 }, // ROL_UPDATE
            new { PermisosId = 4, RolesId = 2 }, // ROL_DELETE
            new { PermisosId = 5, RolesId = 2 }, // USUARIO_CREATE
            new { PermisosId = 6, RolesId = 2 }, // USUARIO_READ
            new { PermisosId = 7, RolesId = 2 }, // USUARIO_UPDATE
            new { PermisosId = 8, RolesId = 2 }, // USUARIO_DELETE
            new { PermisosId = 9, RolesId = 2 }, // PERSONA_CREATE
            new { PermisosId = 10, RolesId = 2 }, // PERSONA_READ
            new { PermisosId = 11, RolesId = 2 }, // PERSONA_UPDATE
            new { PermisosId = 12, RolesId = 2 }, // PERSONA_DELETE
            new { PermisosId = 17, RolesId = 2 }, // DASHBOARD_READ

            // Usuario - Permisos básicos
            new { PermisosId = 6, RolesId = 3 }, // USUARIO_READ (propio)
            new { PermisosId = 7, RolesId = 3 }, // USUARIO_UPDATE (propio)
            new { PermisosId = 10, RolesId = 3 }, // PERSONA_READ
            new { PermisosId = 11, RolesId = 3 }, // PERSONA_UPDATE (propia)
            new { PermisosId = 17, RolesId = 3 }, // DASHBOARD_READ

            // Invitado - Solo lectura
            new { PermisosId = 2, RolesId = 4 }, // ROL_READ
            new { PermisosId = 6, RolesId = 4 }, // USUARIO_READ
            new { PermisosId = 10, RolesId = 4 }  // PERSONA_READ
        );

        // ========== PERSONAS ==========
        var personaAdmin = new Persona
        {
            Id = 1,
            Nombres = "Juan",
            Apellidos = "Pérez",
            FechaNacimiento = new DateTime(1985, 5, 15),
            Genero = "Masculino",
            Telefono = "+1234567890",
            Direccion = "Calle Principal 123",
            Nacionalidad = "Mexicana"
        };

        var personaUsuario = new Persona
        {
            Id = 2,
            Nombres = "María",
            Apellidos = "González",
            FechaNacimiento = new DateTime(1990, 8, 22),
            Genero = "Femenino",
            Telefono = "+9876543210",
            Direccion = "Avenida Central 456",
            Nacionalidad = "Mexicana"
        };

        var personaInvitado = new Persona
        {
            Id = 3,
            Nombres = "Carlos",
            Apellidos = "López",
            FechaNacimiento = new DateTime(1995, 3, 10),
            Genero = "Masculino",
            Telefono = "+1122334455",
            Direccion = "Plaza Mayor 789",
            Nacionalidad = "Mexicana"
        };

        modelBuilder.Entity<Persona>().HasData(personaAdmin, personaUsuario, personaInvitado);
        var pass = BCrypt.Net.BCrypt.HashPassword("123456");
        // ========== USUARIOS ==========
        // Nota: En producción, las contraseñas deben estar hasheadas
        var usuarioSuperAdmin = new Usuario
        {
            Id = 1,
            Nombre = "superadmin",
            Correo = "superadmin@comprasventas.com",
            Password = pass
        };

        var usuarioAdmin = new Usuario
        {
            Id = 2,
            Nombre = "admin",
            Correo = "admin@comprasventas.com",
            Password = pass
        };

        var usuarioRegular = new Usuario
        {
            Id = 3,
            Nombre = "usuario1",
            Correo = "usuario1@comprasventas.com",
            Password = pass
        };

        modelBuilder.Entity<Usuario>().HasData(usuarioSuperAdmin, usuarioAdmin, usuarioRegular);

        // ========== RELACION USUARIO-ROL ==========
        modelBuilder.Entity("usuario_rol").HasData(
            new { RolesId = 1, UsuariosId = 1 }, // Super Admin tiene rol Super Administrador
            new { RolesId = 2, UsuariosId = 2 }, // Admin tiene rol Administrador
            new { RolesId = 3, UsuariosId = 3 }  // Usuario tiene rol Usuario
        );
    }
}
