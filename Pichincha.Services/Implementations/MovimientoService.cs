using AutoMapper;
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
    public class MovimientoService : IMovimientoService
    {
        #region Properties & Members

        private readonly ICuentaRepository _cuentaRepository;
        private readonly IMovimientoRepository _movimientoRepository;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public MovimientoService(IMovimientoRepository MovimientoRepository, ICuentaRepository cuentaRepository, IMapper mapper)
        {
            _movimientoRepository = MovimientoRepository;
            _cuentaRepository = cuentaRepository;
            _mapper = mapper;
        }

        #endregion
        public async Task<IEnumerable<MovimientoReadDto>> GetMovimientos()
        {
            var Movimientos = await _movimientoRepository.GetAllAsync();

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
            var movimiento = await _movimientoRepository.GetAsync(id);
            if (movimiento is null)
                throw new BadRequestException($"Movimiento con Id = {id.ToString()} no existe.");

            return new MovimientoReadDto
            {
                Id = movimiento.Id,
                IdCuenta = movimiento.IdCuenta,
                Fecha = movimiento.FechaCreacion,
                Saldo = movimiento.Saldo,
                TipoMovimiento = movimiento.TipoMovimiento

            };
        }

        public async Task<StatusDto> AddMovimiento(MovimientoCreateDto movimientoDto)
        {
            DateTime date = DateTime.Now;

            var cuentaEnBDD = await _cuentaRepository.GetAsync(movimientoDto.IdCuenta);

            if (cuentaEnBDD is null)
                throw new BadRequestException($"Cuenta con Id = {movimientoDto.IdCuenta} no existe.");

            var movimiento = _mapper.Map<MovimientoEntity>(movimientoDto);

            if (movimiento.TipoMovimiento is null)
                return new StatusDto { IsSuccess = false, Message = "TipoMovimiento no válido." };

            var saldoNuevo = CalcularNuevoSaldo(cuentaEnBDD, movimiento.TipoMovimiento, movimiento.Valor);

            if (saldoNuevo < 0)
            {
                return new StatusDto { IsSuccess = false, Message = $"La cuenta {cuentaEnBDD.NumeroCuenta} no tiene saldo suficiente." };
            }

            cuentaEnBDD.SaldoInicial = saldoNuevo;
            cuentaEnBDD.FechaModificacion = date;

            movimiento.Id = Guid.NewGuid();
            movimiento.Valor = movimiento.Valor;
            movimiento.Saldo = saldoNuevo;
            movimiento.Cuenta = cuentaEnBDD;
            movimiento.FechaModificacion = date;
            movimiento.FechaCreacion = date;

            await _movimientoRepository.AddAsync(movimiento);
            await _movimientoRepository.SaveChangesAsync();

            return new StatusDto { IsSuccess = true, Message = movimiento.Id.ToString() };
        }


        public async Task UpdateMovimiento(Guid id, MovimientoDto dto)
        {
            DateTime date = DateTime.Now;
            var movimiento = await _movimientoRepository.GetAsync(id);
            if (movimiento is null)
                throw new BadRequestException($"Movimiento con Id = {id.ToString()} no existe.");

            movimiento.IdCuenta = dto.IdCuenta;
            movimiento.TipoMovimiento = dto.TipoMovimiento;
            movimiento.Valor = dto.Valor;
            movimiento.Saldo = dto.Saldo;
            movimiento.FechaModificacion = date;

            await _movimientoRepository.UpdateAsync(movimiento);
            await _movimientoRepository.SaveChangesAsync();
        }

        public async Task<StatusDto> RemoveMovimientoById(Guid id)
        {
            StatusDto status = new StatusDto();
            var movimiento = await _movimientoRepository.GetAsync(id);
            if (movimiento is null)
                throw new NotFoundException($"Cuenta con Id = {id.ToString()} no existe.");

            await _movimientoRepository.DeleteAsync(movimiento);
            await _movimientoRepository.SaveChangesAsync();

            status.IsSuccess = true;

            return status;

        }
        public decimal CalcularNuevoSaldo(CuentaEntity cuenta, string tipoMovimiento, decimal valor)
        {
            if (tipoMovimiento == "D")
            {
                cuenta.SaldoInicial -= Math.Abs(valor);
            }
            else if(tipoMovimiento == "C")
            {
                cuenta.SaldoInicial += Math.Abs(valor);
            }

            return cuenta.SaldoInicial;
        }

        public bool CuentaTieneSaldo(MovimientoEntity movimiento, CuentaEntity cuenta)
        {
            if (movimiento.TipoMovimiento == "C")
                return true;

            return Math.Abs(movimiento.Valor) <= cuenta.SaldoInicial;
        }
    }
}
