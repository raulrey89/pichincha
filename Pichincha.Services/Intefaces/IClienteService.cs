using Pichincha.Models.DTOs;
using Pichincha.Models.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Services.Intefaces
{
    public interface IClienteService
    {
        Task<IEnumerable<PersonaClienteReadDto>> GetClientes();
        Task<PersonaClienteReadDto> GetClienteById(Guid id);
        Task<StatusDto> AddCliente(PersonaCliente dto);
        Task UpdateCliente(Guid id, ClienteDto dto);
        Task<StatusDto> RemoveClienteById(Guid id);
    }
}
