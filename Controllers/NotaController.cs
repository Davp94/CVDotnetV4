using ComprasVentas.Dto;
using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController : ControllerBase
    {
        private readonly INotaService _notaService;

        public NotaController(INotaService notaService)
        {
            _notaService = notaService;
        }

        [HttpGet()]
        public async Task<ActionResult<List<NotaDto>>> GetAllNotas()
        {
            var notas = await _notaService.GetAllNotasAsync();
            return Ok(notas);
        }

        [HttpPost]
        public async Task<ActionResult<NotaDto>> CreateNota([FromBody] CreateNotaDto createNotaDto)
        {
            var nota = await _notaService.CreateNotaASync(createNotaDto);
            return Ok(nota);
        }

        [HttpGet("{id}/report")]
        public async Task<ActionResult> GenerateNotaReport(int id)
        {
            var pdfBytes = await _notaService.GenerateNotaReportPdfAsync(id);
            return File(pdfBytes, "application/pdf", $"nota_{id}_report.pdf");
        }
    }
}
