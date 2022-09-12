using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Models.DTOs;
using Pichincha.Models.Request;
using Pichincha.Services.Exceptions;
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

        private readonly IClienteRepository _clienteRepository;

        #endregion

        #region Constructors

        public ClienteService(IClienteRepository ClienteRepository)
        {
            _clienteRepository = ClienteRepository;
        }

        #endregion
        public async Task<IEnumerable<PersonaClienteReadDto>> GetClientes()
        {
            var clientes = await _clienteRepository.GetAllAsync();

            return clientes.Select(x => new PersonaClienteReadDto
            {
                Id = x.Id,
                Direccion = x.Direccion,
                Edad = x.Edad,
                Genero = x.Genero,
                Identificacion = x.Identificacion,
                Nombre = x.Nombre,
                Estado = x.Estado
            });
        }

        public async Task<PersonaClienteReadDto> GetClienteById(Guid id)
        {
            var cliente = await _clienteRepository.GetAsync(id);
            if (cliente is null)
                throw new BadRequestException($"Cliente con Id = {id.ToString()} no existe.");

            return new PersonaClienteReadDto
            {
                Id = cliente.Id,
                Direccion = cliente.Direccion,
                Edad = cliente.Edad,
                Genero = cliente.Genero,
                Identificacion = cliente.Identificacion,
                Nombre = cliente.Nombre,
                Estado = cliente.Estado

            };
        }

        public async Task AddCliente(PersonaCliente dto)
        {
            DateTime date = DateTime.Now;

            var clienteEntity = new ClienteEntity
            {
                Id = Guid.NewGuid(),
                Contrasena = dto.Contrasena,
                Estado = dto.Estado,
                Edad = dto.Edad,
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Genero = dto.Genero,
                Identificacion = dto.Identificacion,
                Telefono = dto.Telefono,
                FechaModificacion = date,
                FechaCreacion = date
            };

            await _clienteRepository.AddAsync(clienteEntity);
            await _clienteRepository.SaveChangesAsync();
                        
        }


        public async Task UpdateCliente(Guid id, ClienteDto dto)
        {
            DateTime date = DateTime.Now;

            var cliente = await _clienteRepository.GetAsync(id);
            if (cliente is null)
                throw new BadRequestException($"Cliente con Id = {id.ToString()} no existe.");

            cliente.Contrasena = dto.Contrasena;
            cliente.Estado = dto.Estado;
            cliente.FechaModificacion = date;

            await _clienteRepository.UpdateAsync(cliente);
            await _clienteRepository.SaveChangesAsync();
        }

        public async Task<StatusDto> RemoveClienteById(Guid id)
        {
            StatusDto status = new StatusDto();
            try
            {
                var cliente = await _clienteRepository.GetAsync(id);
                if (cliente is null)
                    throw new NotFoundException($"Cliente con Id = {id.ToString()} no existe.");

                await _clienteRepository.DeleteAsync(cliente);
                await _clienteRepository.SaveChangesAsync();

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
