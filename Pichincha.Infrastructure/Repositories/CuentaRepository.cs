using Microsoft.EntityFrameworkCore;
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
        public async Task<ClienteEntity> GetReportePorFechas(Guid clienteId, DateTime fechaIni, DateTime fechaFin)
        {
            return await _context.Cliente
                    .Where(con => con.Id == clienteId)
                    .Include(c => c.Cuentas)
                    .ThenInclude(cue => cue.Movimientos
                        .Where(mov => mov.FechaCreacion <= fechaFin && mov.FechaCreacion >= fechaIni))
                    .FirstOrDefaultAsync();
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
