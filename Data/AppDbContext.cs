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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rol>()
            .HasMany(r=>r.Permisos)
            .WithMany(p=>p.Roles)
            .UsingEntity(q=> q.ToTable("permiso_rol"));

        // modelBuilder.Entity<Rol>()
        //     .HasIndex(r=>r.Nombre)
        //     .IsUnique()
        //     .HasDatabaseName("idx_rol_nombre_unique");   

        modelBuilder.Entity<Usuario>()
            .HasOne(u=>u.Persona)
            .WithOne(p=>p.Usuario)
            .HasForeignKey<Persona>(p=>p.Id); 
    }
}
