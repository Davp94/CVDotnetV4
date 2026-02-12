using System;

namespace ComprasVentas.Services.spec;

public interface IFileService
{
    Task<string> SaveFileAsync(IFormFile file);

    Task<Stream> GetFileAsync(string filePath);

}
