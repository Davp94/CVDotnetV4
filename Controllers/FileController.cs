using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<string>> UploadFile(IFormFile file)
        {
            try
            {
                var path = await _fileService.SaveFileAsync(file);
                if(path == null)
                    return BadRequest("Error al guardar el archivo");
                return Ok(new { filepath = path});    
            }
            catch (Exception ex)
            { 
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadFile([FromQuery] string filePath)
        {
            try
            {
                var stream = await _fileService.GetFileAsync(filePath);
                if(stream == null)
                    return NotFound("Archivo no encontrado");
                
                var contentType = "application/octet-stream";
                var fileName = Path.GetFileName(filePath);
                return File(stream, contentType, fileName);
            }
            catch (FileNotFoundException)
            {
                return NotFound("Archivo no encontrado");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
