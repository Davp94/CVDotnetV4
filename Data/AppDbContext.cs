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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>()
            .HasMany(r=>r.Permisos)
            .WithMany(p=>p.Roles)
            .UsingEntity(q=> q.ToTable("permiso_rol"));

        modelBuilder.Entity<Usuario>()
            .HasMany(r=>r.Roles)
            .WithMany(p=>p.Usuarios)
            .UsingEntity(q=> q.ToTable("usuario_rol"));
   
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
    }
}
