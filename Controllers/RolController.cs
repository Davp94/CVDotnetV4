using ComprasVentas.Dto;
using ComprasVentas.Services.spec;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComprasVentas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController(IRolService rolService) : ControllerBase
    {

        private readonly IRolService _rolService = rolService;

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<RolDto>>> GetAll()
        {
            return Ok(await rolService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolDto>> GetById(int id)
        {
            var rol = await _rolService.GetByIdAsync(id);
            return rol != null ? Ok(rol) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CreateRolDto rolDto)
        {
            try
            {
                var createdRol = await rolService.CreateAsync(rolDto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception)
            {
                
                throw new Exception("Error al crear el rol");
            }
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreateRolDto rolDto)
        {
            await _rolService.UpdateAsync(id, rolDto);
            return NoContent();   
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _rolService.DeleteAsync(id);
            return NoContent();   
        }      
      

    }
}
