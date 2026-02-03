using System;
using ComprasVentas.Common;
using ComprasVentas.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace ComprasVentas.Middleware;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(exception, "Error en proceso logged");

        var response = new ErrorResponse
        {
            Timestamp = DateTime.UtcNow,
            Path = httpContext.Request.Path,
        };

        switch (exception)
        {
            case NotFoundException notfoundEx:
                response.StatusCode = StatusCodes.Status404NotFound;
                response.Message = notfoundEx.Message;
                break;
            case UnauthorizedAccessException:
                response.StatusCode = StatusCodes.Status401Unauthorized;
                response.Message = "No autorizado";
                break;  
            default:
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Message = "Error interno del servidor";
                break;
        }

        httpContext.Response.StatusCode = response.StatusCode;
        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

        return true;
    }
}
