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

        #endregion

        #region Constructors

        public CuentaService(ICuentaRepository CuentaRepository, IClienteRepository clienteRepository)
        {
            _CuentaRepository = CuentaRepository;
            _clienteRepository = clienteRepository;
        }

        #endregion
        public async Task<IEnumerable<CuentaReadDto>> GetCuentas()
        {
            var cuentas = await _CuentaRepository.GetAllAsync();

            return cuentas.Select(x => new CuentaReadDto
            {
                Id = x.Id,
                NumeroCuenta = x.NumeroCuenta,
                IdCliente = x.IdCliente,
                Estado = x.Estado,
                SaldoInicial = x.SaldoInicial,
                TipoCuenta = x.TipoCuenta
            });
        }

        public async Task<CuentaReadDto> GetCuentaById(Guid id)
        {
            var cuenta = await _CuentaRepository.GetAsync(id);
            if (cuenta is null)
                throw new BadRequestException($"Cuenta con Id = {id.ToString()} no existe.");

            return new CuentaReadDto
            {
                Id = cuenta.Id,
                NumeroCuenta = cuenta.NumeroCuenta,
                IdCliente = cuenta.IdCliente,
                Estado = cuenta.Estado,
                SaldoInicial = cuenta.SaldoInicial,
                TipoCuenta = cuenta.TipoCuenta

            };

        }

        public async Task<StatusDto> AddCuenta(CuentaDto dto)
        {
            var clienteEnBdd = await _clienteRepository.GetAsync(dto.IdCliente);

            if (clienteEnBdd is null)
                throw new BadRequestException($"Cliente con Id = {dto.IdCliente} no existe.");

            DateTime date = DateTime.Now;
            var cuentaEntity = new CuentaEntity
            {
                NumeroCuenta = dto.NumeroCuenta,
                IdCliente = dto.IdCliente,
                Estado = dto.Estado,
                SaldoInicial = dto.SaldoInicial,
                TipoCuenta = dto.TipoCuenta,
                FechaModificacion = date,
                FechaCreacion = date
            };

            await _CuentaRepository.AddAsync(cuentaEntity); 
            await _CuentaRepository.SaveChangesAsync();


            return new StatusDto { IsSuccess = true };
        }


        public async Task UpdateCuenta(Guid id, CuentaDto dto)
        {
            DateTime date = DateTime.Now;
            var cuenta = await _CuentaRepository.GetAsync(id);
            if (cuenta is null)
                throw new BadRequestException($"Cuenta con Id = {id.ToString()} no existe.");

            cuenta.NumeroCuenta = dto.NumeroCuenta;
            cuenta.IdCliente = dto.IdCliente;
            cuenta.Estado = dto.Estado;
            cuenta.SaldoInicial = dto.SaldoInicial;
            cuenta.TipoCuenta = dto.TipoCuenta;
            cuenta.FechaModificacion = date;

            await _CuentaRepository.UpdateAsync(cuenta);
            await _CuentaRepository.SaveChangesAsync();
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

            var result = await ObtenerFormatoSalida(cuenta);
            return result;
        }

        public Task<List<ReporteDto>> ObtenerFormatoSalida(ClienteEntity cliente)
        {
            var reporte = (from cuenta in cliente.Cuentas
                    from movimiento in cuenta.Movimientos
                    let item = new ReporteDto
                    {
                        Cliente = cliente.Nombre,
                        NumeroCuenta = cuenta.NumeroCuenta ?? "",
                        Tipo = cuenta.TipoCuenta ?? "",
                        Fecha = movimiento.FechaCreacion,
                        SaldoInicial = movimiento.Saldo - movimiento.Valor,
                        SaldoDisponible = movimiento.Saldo,
                        Movimiento = movimiento.Valor,
                        Estado = movimiento.Estado ?? true
                    }
                    select item).ToList();

            return Task.FromResult(reporte);
        }
    }
}
