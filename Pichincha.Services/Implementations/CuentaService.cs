using Pichincha.Domain.Common;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Models.DTOs;
using Pichincha.Services.Exceptions;
using Pichincha.Services.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Services.Implementations
{
    public class CuentaService : ICuentaService
    {
        #region Properties & Members

        private readonly ICuentaRepository _CuentaRepository;
        private readonly IClienteRepository _clienteRepository;
        private readonly IMovimientoRepository _movimientoRepository;

        #endregion

        #region Constructors

        public CuentaService(ICuentaRepository CuentaRepository, IClienteRepository clienteRepository, IMovimientoRepository movimientoRepository)
        {
            _CuentaRepository = CuentaRepository;
            _clienteRepository = clienteRepository;
            _movimientoRepository = movimientoRepository;
        }

        #endregion
        public async Task<IEnumerable<CuentaReadDto>> GetCuentas()
        {
            var cuentas = await _CuentaRepository.GetAllCuentasCliente();
            return cuentas;
        }

        public async Task<CuentaReadDto> GetCuentaById(Guid id)
        {
            var cuenta = await _CuentaRepository.GetCuentaClienteById(id);
            if (cuenta is null)
                throw new BadRequestException($"Cuenta con Id = {id} no existe.");

            return cuenta;
        }

        public async Task<StatusDto> AddCuenta(CuentaDto dto)
        {
            var clienteEnBdd = await _clienteRepository.GetAsync(dto.IdCliente);

            if (clienteEnBdd is null)
                throw new BadRequestException($"Cliente con Id = {dto.IdCliente} no existe.");

            DateTime date = DateTime.UtcNow;
            Guid idCuenta = Guid.NewGuid();
            var cuentaEntity = new CuentaEntity
            {
                Id = idCuenta,
                NumeroCuenta = dto.NumeroCuenta,
                IdCliente = dto.IdCliente,
                Estado = dto.Estado,
                Saldo = dto.SaldoInicial,
                TipoCuenta = dto.TipoCuenta,
                FechaModificacion = date,
                FechaCreacion = date
            };

            var movimiento = new MovimientoEntity
            {
                Id = Guid.NewGuid(),
                IdCuenta = idCuenta,
                TipoMovimiento = TipoMovimientos.C.ToString(),
                Valor = dto.SaldoInicial,
                Saldo = dto.SaldoInicial,
                FechaModificacion = date,
                FechaCreacion = date
            };

            await _movimientoRepository.AddAsync(movimiento);

            await _CuentaRepository.AddAsync(cuentaEntity); 

            await _CuentaRepository.SaveChangesAsync();

            return new StatusDto { IsSuccess = true };
        }


        public async Task<StatusDto> UpdateCuenta(Guid id, CuentaDto dto)
        {
            DateTime date = DateTime.UtcNow;
            var cuenta = await _CuentaRepository.GetAsync(id);
            if (cuenta is null)
                throw new BadRequestException($"Cuenta con Id = {id.ToString()} no existe.");

            cuenta.NumeroCuenta = dto.NumeroCuenta;
            cuenta.IdCliente = dto.IdCliente;
            cuenta.Estado = dto.Estado;
            cuenta.Saldo = dto.SaldoInicial;
            cuenta.TipoCuenta = dto.TipoCuenta;
            cuenta.FechaModificacion = date;

            await _CuentaRepository.UpdateAsync(cuenta);
            await _CuentaRepository.SaveChangesAsync();

            return new StatusDto { IsSuccess = true };
        }

        public async Task<StatusDto> RemoveCuentaById(Guid id)
        {
            StatusDto status = new StatusDto();
            var cuenta = await _CuentaRepository.GetAsync(id);
            if (cuenta is null)
                throw new NotFoundException($"Cuenta con Id = {id.ToString()} no existe.");

            await _CuentaRepository.DeleteAsync(cuenta);
            await _CuentaRepository.SaveChangesAsync();

            status.IsSuccess = true;

            return status;
        }
        public async Task<List<ReporteDto>> GetReportePorFechas(Guid clienteId, DateTime fechaIni, DateTime fechaFin)
        {
            var cuenta = await _CuentaRepository.GetReportePorFechas(clienteId, fechaIni, fechaFin);
            var listaOrdenada = cuenta.OrderBy(o => o.NumeroCuenta).ThenBy(t => t.Fecha).ToList();
            return listaOrdenada;
        }
    }
}
