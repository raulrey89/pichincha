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
                Telefono = x.Telefono,
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
                Telefono = cliente.Telefono,
                Estado = cliente.Estado

            };
        }

        public async Task<StatusDto> AddCliente(PersonaCliente dto)
        {
            DateTime date = DateTime.UtcNow;

            var clienteEntity = new ClienteEntity
            {
                Id = Guid.NewGuid(),
                Contrasena = dto.Contrasena,
                Estado = dto.Estado,
                Nombre = dto.Nombre,
                Edad = dto.Edad,
                Direccion = dto.Direccion,
                Genero = dto.Genero,
                Identificacion = dto.Identificacion,
                Telefono = dto.Telefono,
                FechaModificacion = date,
                FechaCreacion = date
            };

            await _clienteRepository.AddAsync(clienteEntity);
            await _clienteRepository.SaveChangesAsync();

            return new StatusDto { IsSuccess = true };
                        
        }


        public async Task<StatusDto> UpdateCliente(Guid id, PersonaCliente dto)
        {
            DateTime date = DateTime.UtcNow;

            var cliente = await _clienteRepository.GetAsync(id);
            if (cliente is null)
                throw new BadRequestException($"Cliente con Id = {id} no existe.");
            
            cliente.Nombre = dto.Nombre;
            cliente.Edad = dto.Edad;
            cliente.Direccion = dto.Direccion;
            cliente.Genero = dto.Genero;
            cliente.Identificacion = dto.Identificacion;
            cliente.Telefono = dto.Telefono;
            cliente.Contrasena = dto.Contrasena;
            cliente.Estado = dto.Estado;
            cliente.FechaModificacion = date;

            await _clienteRepository.UpdateAsync(cliente);
            await _clienteRepository.SaveChangesAsync();

            return new StatusDto { IsSuccess = true };
        }

        public async Task<StatusDto> RemoveClienteById(Guid id)
        {
            StatusDto status = new StatusDto();
            var cliente = await _clienteRepository.GetAsync(id);
            if (cliente is null)
                throw new NotFoundException($"Cliente con Id = {id.ToString()} no existe.");

            await _clienteRepository.DeleteAsync(cliente);
            await _clienteRepository.SaveChangesAsync();

            status.IsSuccess = true;

            return status;
        }
    }
}
