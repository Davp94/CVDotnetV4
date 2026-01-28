using ComprasVentas.Data;
using ComprasVentas.Repository;
using ComprasVentas.Services.impl;
using ComprasVentas.Services.spec;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? throw new InvalidOperationException("Conexion no encontrada")));
// Add services to the container.
builder.Services.AddScoped<PermisoRepository>();
builder.Services.AddScoped<RolRepository>();
builder.Services.AddScoped<IPermisoService, PermisoService>();
builder.Services.AddScoped<IRolService, RolService>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
