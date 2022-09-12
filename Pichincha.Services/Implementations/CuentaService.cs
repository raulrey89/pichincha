using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Models.DTOs;
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

        #endregion

        #region Constructors

        public CuentaService(ICuentaRepository CuentaRepository)
        {
            _CuentaRepository = CuentaRepository;
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
            var Cuenta = await _CuentaRepository.GetAsync(id);

            return new CuentaReadDto
            {
                Id = Cuenta.Id,
                NumeroCuenta = Cuenta.NumeroCuenta,
                IdCliente = Cuenta.IdCliente,
                Estado = Cuenta.Estado,
                SaldoInicial = Cuenta.SaldoInicial,
                TipoCuenta = Cuenta.TipoCuenta

            };
        }

        public async Task AddCuenta(CuentaDto dto)
        {
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
        }


        public async Task UpdateCuenta(Guid id, CuentaDto dto)
        {
            DateTime date = DateTime.Now;
            var cuenta = await _CuentaRepository.GetAsync(id);
            cuenta.NumeroCuenta = dto.NumeroCuenta;
            cuenta.IdCliente = dto.IdCliente;
            cuenta.Estado = dto.Estado;
            cuenta.SaldoInicial = dto.SaldoInicial;
            cuenta.TipoCuenta = dto.TipoCuenta;
            cuenta.FechaModificacion = date;

            await _CuentaRepository.UpdateAsync(cuenta);
        }

        public async Task<StatusDto> RemoveCuentaById(Guid id)
        {
            StatusDto status = new StatusDto();
            try
            {

                var Cuenta = await _CuentaRepository.GetAsync(id);

                await _CuentaRepository.DeleteAsync(Cuenta);

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
