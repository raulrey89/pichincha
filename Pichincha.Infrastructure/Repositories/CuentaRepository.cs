using Microsoft.EntityFrameworkCore;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Database;
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
    }
}
