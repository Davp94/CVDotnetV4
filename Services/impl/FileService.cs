using System;
using ComprasVentas.Services.spec;

namespace ComprasVentas.Services.impl;

public class FileService : IFileService
{
    private readonly IConfiguration _configuration;

    private readonly IWebHostEnvironment _environment;

    public FileService(IConfiguration configuration, IWebHostEnvironment environment)
    {
        _configuration = configuration;
        _environment = environment;
    }

    public Task<Stream> GetFileAsync(string filePath)
    {
        var fullPath = Path.Combine(_environment.ContentRootPath, filePath);
        if(!File.Exists(fullPath))
        {
            throw new FileNotFoundException("Archivo no encontrado", fullPath);
        }

        return Task.FromResult<Stream>(new FileStream(fullPath, FileMode.Open, FileAccess.Read));
    }

    public async Task<string> SaveFileAsync(IFormFile file)
    {
        if(file == null || file.Length == 0)
        {
            throw new ArgumentException("File is null or empty");
        }

        var storagePath = _configuration.GetValue<string>("Storage:ImageDirectory");

        var uploadPath = Path.Combine(_environment.ContentRootPath, storagePath);

        if(!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        var fileName = $"{Guid.NewGuid()}_{file.FileName}";
        var filePath = Path.Combine(uploadPath, fileName);

        using(var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var relative = Path.Combine(storagePath, fileName);
        return relative.Replace("\\", "/");


    }
}
