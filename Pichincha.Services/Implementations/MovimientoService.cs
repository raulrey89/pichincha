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
    public class MovimientoService : IMovimientoService
    {
        #region Properties & Members

        private readonly IMovimientoRepository _MovimientoRepository;

        #endregion

        #region Constructors

        public MovimientoService(IMovimientoRepository MovimientoRepository)
        {
            _MovimientoRepository = MovimientoRepository;
        }

        #endregion
        public async Task<IEnumerable<MovimientoReadDto>> GetMovimientos()
        {
            var Movimientos = await _MovimientoRepository.GetAllAsync();

            return Movimientos.Select(x => new MovimientoReadDto
            {
                Id = x.Id,
                IdCuenta = x.IdCuenta,
                Fecha = x.FechaCreacion,
                TipoMovimiento = x.TipoMovimiento,
                Valor = x.Valor,
                Saldo = x.Saldo
            });
        }

        public async Task<MovimientoReadDto> GetMovimientoById(Guid id)
        {
            var Movimiento = await _MovimientoRepository.GetAsync(id);

            return new MovimientoReadDto
            {
                Id = Movimiento.Id,
                IdCuenta = Movimiento.IdCuenta,
                Fecha = Movimiento.FechaCreacion,
                Saldo = Movimiento.Saldo,
                TipoMovimiento = Movimiento.TipoMovimiento

            };
        }

        public async Task AddMovimiento(MovimientoDto dto)
        {
            DateTime date = DateTime.Now;
            var movimientoEntity = new MovimientoEntity
            {
                IdCuenta = dto.IdCuenta,
                TipoMovimiento = dto.TipoMovimiento,
                Saldo = dto.Saldo,
                FechaModificacion = date,
                FechaCreacion = date
            };

            await _MovimientoRepository.AddAsync(movimientoEntity);
            await _MovimientoRepository.SaveChangesAsync();
        }


        public async Task UpdateMovimiento(Guid id, MovimientoDto dto)
        {
            DateTime date = DateTime.Now;
            var Movimiento = await _MovimientoRepository.GetAsync(id);
            Movimiento.IdCuenta = dto.IdCuenta;
            Movimiento.TipoMovimiento = dto.TipoMovimiento;
            Movimiento.Valor = dto.Valor;
            Movimiento.Saldo = dto.Saldo;
            Movimiento.FechaModificacion = date;

            await _MovimientoRepository.UpdateAsync(Movimiento);
            await _MovimientoRepository.SaveChangesAsync();
        }

        public async Task<StatusDto> RemoveMovimientoById(Guid id)
        {
            StatusDto status = new StatusDto();
            try
            {

                var Movimiento = await _MovimientoRepository.GetAsync(id);

                await _MovimientoRepository.DeleteAsync(Movimiento);
                await _MovimientoRepository.SaveChangesAsync();

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
