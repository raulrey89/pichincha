using Microsoft.EntityFrameworkCore;
using Pichincha.Domain.Common;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Database;
using Pichincha.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Infrastructure.Repositories
{
    public class CuentaRepository : RepositoryBase<CuentaEntity>, ICuentaRepository
    {
        private readonly AppDbContext _context;
        public CuentaRepository(AppDbContext unitOfWork) : base(unitOfWork)
        {
            _context = unitOfWork;
        }

        public async Task<List<ReporteDto>> GetReportePorFechas(Guid clienteId, DateTime fechaIni, DateTime fechaFin)
        {
            List<ReporteDto> reporte = await (from cliente in _context.Cliente
                           join cuenta in _context.Cuenta on cliente.Id equals cuenta.IdCliente
                           join movimiento in _context.Movimiento on cuenta.Id equals movimiento.IdCuenta
                           where 
                                cliente.Id == clienteId 
                                && movimiento.FechaCreacion >= fechaIni
                                && movimiento.FechaCreacion <= fechaFin 
                           select new ReporteDto
                           {
                               Cliente = cliente.Nombre,
                               NumeroCuenta = cuenta.NumeroCuenta ?? "",
                               Tipo = cuenta.TipoCuenta ?? "",
                               Fecha = movimiento.FechaCreacion,
                               SaldoInicial = movimiento.Saldo - movimiento.Valor,
                               SaldoDisponible = movimiento.Saldo,
                               Movimiento = (movimiento.TipoMovimiento == TipoMovimientos.D.ToString() ? "Débito":"Crédito") +" de "+  movimiento.Valor,
                               Estado = movimiento.Estado ?? true
                           }).ToListAsync();

            return reporte;
        }

        public async  Task<List<CuentaReadDto>> GetAllCuentasCliente()
        {
            var cuentas = await (from cu in _context.Cuenta
                                 join cli in _context.Cliente on cu.IdCliente equals cli.Id
                                 select new CuentaReadDto
                                 {
                                     Id = cu.Id,
                                     NumeroCuenta = cu.NumeroCuenta,
                                     NombreCliente = cli.Nombre,
                                     Estado = cu.Estado,
                                     SaldoInicial = cu.Saldo,
                                     TipoCuenta = cu.TipoCuenta

                                 }).ToListAsync();

            return cuentas;
        }

        public async Task<CuentaReadDto> GetCuentaClienteById(Guid id)
        {
            var cuenta = await (from cu in _context.Cuenta
                                 join cli in _context.Cliente on cu.IdCliente equals cli.Id
                                 where cu.Id == id
                                 select new CuentaReadDto
                                 {
                                     Id = cu.Id,
                                     NumeroCuenta = cu.NumeroCuenta,
                                     NombreCliente = cli.Nombre,
                                     Estado = cu.Estado,
                                     SaldoInicial = cu.Saldo,
                                     TipoCuenta = cu.TipoCuenta

                                 }).FirstOrDefaultAsync();

            return cuenta;
        }
    }
}
