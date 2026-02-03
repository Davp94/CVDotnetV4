using System;

namespace ComprasVentas.Common;

public class ErrorResponse
{
    //public string TraceId { get; set; } = string.Empty;
    public int StatusCode { get; set; }

    public string Message { get; set; } = string.Empty;

    public DateTime Timestamp { get; set; }

    public string Path { get; set; } = string.Empty;

    public List<string> Errors { get; set; } = [];
}
