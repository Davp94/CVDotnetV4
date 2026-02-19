using System;
using ComprasVentas.Dto;

namespace ComprasVentas.Services.spec;

public interface INotaService
{
    Task<List<NotaDto>> GetAllNotasAsync();

    Task<NotaDto> CreateNotaASync(CreateNotaDto notaRequestDto);

    Task<byte[]> GenerateNotaReportPdfAsync(int notaId);
}
