using ComprasVentas.Common;
using ComprasVentas.Data;
using ComprasVentas.Middleware;
using ComprasVentas.Repository;
using ComprasVentas.Services.impl;
using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Mvc;
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
builder.Services.AddScoped<UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<ITokenService , TokenService>();
builder.Services.AddScoped<IAuthService , AuthService>();

builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();
builder.Services.AddControllers()
 .ConfigureApiBehaviorOptions(options =>
 {
     options.InvalidModelStateResponseFactory = context =>
     {
         var errors = context.ModelState.Values
             .SelectMany(v => v.Errors)
             .Select(e => e.ErrorMessage)
             .ToList();
         var response = new ErrorResponse
         {
             StatusCode = StatusCodes.Status400BadRequest,
             Message = "Errores en validacion",
             Timestamp = DateTime.UtcNow,
             Path = context.HttpContext.Request.Path,
             Errors = errors
         };
         return new BadRequestObjectResult(response);
     };
 });
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
