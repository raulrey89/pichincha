using Pichincha.Domain.Entities;
using Pichincha.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain.Interfaces
{
    public interface ICuentaRepository : IRepository<CuentaEntity>
    {
        Task<List<CuentaReadDto>> GetAllCuentasCliente();
        Task<CuentaReadDto> GetCuentaClienteById(Guid id);
        Task<List<ReporteDto>> GetReportePorFechas(Guid clienteId, DateTime fechaIni, DateTime fechaFin);
    }
}
