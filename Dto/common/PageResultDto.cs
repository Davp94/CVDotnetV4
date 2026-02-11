using System;

namespace ComprasVentas.Dto.common;

public class PageResultDto<T>
{
    public IEnumerable<T> Items { get; set; } = [];
    public int TotalCount { get; set; }

    public int Page { get; set; }

    public int Size { get; set; }  

    public int TotalPages => (int)Math.Ceiling((double)TotalCount / Size);

}
