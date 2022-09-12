using Pichincha.Domain.Entities;
using Pichincha.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Services.Intefaces
{
    public interface ICuentaService
    {
        Task<IEnumerable<CuentaReadDto>> GetCuentas();
        Task<CuentaReadDto> GetCuentaById(Guid id);
        Task AddCuenta(CuentaDto dto);
        Task UpdateCuenta(Guid id, CuentaDto dto);
        Task<StatusDto> RemoveCuentaById(Guid id);
        Task<List<ReporteDto>> GetReportePorFechas(Guid clienteId, DateTime fechaIni, DateTime fechaFin);
    }
}
