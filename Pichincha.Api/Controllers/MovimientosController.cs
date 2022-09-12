using Microsoft.AspNetCore.Mvc;
using Pichincha.Models.DTOs;
using Pichincha.Services.Intefaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pichincha.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosController : ControllerBase
    {
        private readonly IMovimientoService _MovimientoService;
        public MovimientosController(IMovimientoService MovimientoService)
        {
            _MovimientoService = MovimientoService;
        }

        // GET: api/<MovimientoController>
        [HttpGet]
        public async Task<IEnumerable<MovimientoReadDto>> Get()
        {
            var result = await _MovimientoService.GetMovimientos();
            return result;
        }

        // GET api/<MovimientoController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            MovimientoReadDto result = await _MovimientoService.GetMovimientoById(id);
            return Ok(result);
        }

        // POST api/<MovimientoController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovimientoDto Movimiento)
        {
            await _MovimientoService.AddMovimiento(Movimiento);

            return Ok();
        }

        // PUT api/<MovimientoController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] MovimientoDto Movimiento)
        {
            StatusDto status = new StatusDto();
            MovimientoReadDto result = await _MovimientoService.GetMovimientoById(id);
            if (result is null)
            {
                return;
            }

            await _MovimientoService.UpdateMovimiento(id, Movimiento);

            return;
        }

        // DELETE api/<MovimientoController>/5
        [HttpDelete("{id}")]
        public async Task<StatusDto> Delete(Guid id)
        {

            StatusDto status = new StatusDto();
            MovimientoReadDto result = await _MovimientoService.GetMovimientoById(id);
            if (result is null)
            {
                status = new StatusDto { IsSuccess = false, Message = $"Movimiento {id} is not valid" };
                return status;
            }

            status = await _MovimientoService.RemoveMovimientoById(id);

            return status;
        }
    }
}
