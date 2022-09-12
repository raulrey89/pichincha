using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Models.DTOs;
using Pichincha.Models.Request;
using Pichincha.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Services.Implementations
{
    public class ClienteService : IClienteService
    {
        #region Properties & Members

        private readonly IPersonaRepository _personaRepository;
        private readonly IClienteRepository _clienteRepository;

        #endregion

        #region Constructors

        public ClienteService(IClienteRepository ClienteRepository, IPersonaRepository personaRepository)
        {
            _personaRepository = personaRepository;
            _clienteRepository = ClienteRepository;
        }

        #endregion
        public async Task<IEnumerable<PersonaClienteReadDto>> GetClientes()
        {
            var clientes = await _clienteRepository.GetAllAsync();

            return clientes.Select(x => new PersonaClienteReadDto
            {
                IdCliente = x.Id,
                IdPersona = x.IdPersona,
                Estado = x.Estado
            });
        }

        public async Task<PersonaClienteReadDto> GetClienteById(Guid id)
        {
            var clientes = await _clienteRepository.GetAsync(id);

            return new PersonaClienteReadDto
            {
                IdCliente = clientes.Id,
                IdPersona = clientes.IdPersona,
                Estado = clientes.Estado,

            };
        }

        public async Task AddCliente(PersonaCliente dto)
        {
            try
            {
                DateTime date = DateTime.Now;
                Guid idPersona = Guid.NewGuid();

                var personaEntity = new PersonaEntity
                {
                    Id = idPersona,
                    Edad = dto.Edad,
                    Nombre = dto.Nombre,
                    Direccion = dto.Direccion,
                    Genero = dto.Genero,
                    Identificacion = dto.Identificacion,
                    Telefono = dto.Telefono,
                    FechaModificacion = date,
                    FechaCreacion = date
                };

                await _personaRepository.AddAsync(personaEntity);

                var clienteEntity = new ClienteEntity
                {
                    Id = Guid.NewGuid(),
                    IdPersona = idPersona,
                    Contrasena = dto.Contrasena,
                    Estado = dto.Estado,
                    FechaModificacion = date,
                    FechaCreacion = date
                };

                await _clienteRepository.AddAsync(clienteEntity);

            }
            catch (Exception ex)
            {
                var x = ex.Message;
            }
        }


        public async Task UpdateCliente(Guid id, ClienteDto dto)
        {
            DateTime date = DateTime.Now;

            var cliente = await _clienteRepository.GetAsync(id);
            cliente.IdPersona = dto.IdPersona;
            cliente.Contrasena = dto.Contrasena;
            cliente.Estado = dto.Estado;
            cliente.FechaModificacion = date;

            await _clienteRepository.UpdateAsync(cliente);
        }

        public async Task<StatusDto> RemoveClienteById(Guid id)
        {
            StatusDto status = new StatusDto();
            try
            {
                var cliente = await _clienteRepository.GetAsync(id);

                await _clienteRepository.RemoveAsync(cliente);

                status.IsSuccess = true;

                return status;
            }
            catch (Exception ex)
            {

                status.IsSuccess = false;
                status.Message = ex.Message;
                return status;
            }
        }
    }
}
