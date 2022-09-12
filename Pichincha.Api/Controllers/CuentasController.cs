using Microsoft.AspNetCore.Mvc;
using Pichincha.Models.DTOs;
using Pichincha.Services.Intefaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pichincha.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
        public CuentasController(ICuentaService CuentaService)
        {
            _cuentaService = CuentaService;
        }

        // GET: api/<CuentaController>
        [HttpGet]
        public async Task<IEnumerable<CuentaReadDto>> Get()
        {
            var result = await _cuentaService.GetCuentas();
            return result;
        }

        // GET api/<CuentaController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            CuentaReadDto result = await _cuentaService.GetCuentaById(id);
            return Ok(result);
        }

        // POST api/<CuentaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CuentaDto Cuenta)
        {
            await _cuentaService.AddCuenta(Cuenta);

            return Ok();
        }

        // PUT api/<CuentaController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] CuentaDto Cuenta)
        {
            await _cuentaService.UpdateCuenta(id, Cuenta);

            return;
        }

        // DELETE api/<CuentaController>/5
        [HttpDelete("{id}")]
        public async Task<StatusDto> Delete(Guid id)
        {

            StatusDto status = new StatusDto();

            status = await _cuentaService.RemoveCuentaById(id);

            return status;
        }
    }
}
