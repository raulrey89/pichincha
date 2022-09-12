using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pichincha.Services.Intefaces;

namespace Pichincha.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;

        public ReportesController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid clienteId, DateTime fechaIni, DateTime fechaFin)
        {
            var datosParaReporte = await _cuentaService.GetReportePorFechas(clienteId, fechaIni, fechaFin);

            return Ok(datosParaReporte);
        }
    }
}
