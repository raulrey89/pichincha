using Microsoft.AspNetCore.Mvc;
using Pichincha.Models.DTOs;
using Pichincha.Models.Request;
using Pichincha.Services.Intefaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Pichincha.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;
        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // GET: api/<ClienteController>
        [HttpGet]
        public async Task<IEnumerable<PersonaClienteReadDto>> Get()
        {
            var result = await _clienteService.GetClientes();
            return result;
        }

        // GET api/<ClienteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            PersonaClienteReadDto result = await _clienteService.GetClienteById(id);
            return Ok(result);
        }

        // POST api/<ClienteController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonaCliente Cliente)
        {
            await _clienteService.AddCliente(Cliente);

            return Ok();
        }

        // PUT api/<ClienteController>/5
        [HttpPut("{id}")]
        public async Task Put(Guid id, [FromBody] ClienteDto Cliente)
        {
            StatusDto status = new StatusDto();
            PersonaClienteReadDto result = await _clienteService.GetClienteById(id);
            if (result is null)
            {
                return;
            }

            await _clienteService.UpdateCliente(id, Cliente);

            return;
        }

        // DELETE api/<ClienteController>/5
        [HttpDelete("{id}")]
        public async Task<StatusDto> Delete(Guid id)
        {

            StatusDto status = new StatusDto();
            PersonaClienteReadDto result = await _clienteService.GetClienteById(id);
            if (result is null)
            {
                status = new StatusDto { IsSuccess = false, Message = $"Cliente {id} is not valid" };
                return status;
            }

            status = await _clienteService.RemoveClienteById(id);

            return status;
        }
    }
}
