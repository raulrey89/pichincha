using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Models.DTOs
{
    public class ReporteDto
    {
        public DateTime Fecha { get; set; }
        public string Cliente { get; set; } = null!;
        public string NumeroCuenta { get; set; } = null!;
        public string Tipo { get; set; } = null!;
        public Decimal SaldoInicial { get; set; }
        public bool Estado { get; set; }
        public string Movimiento { get; set; }
        public decimal SaldoDisponible { get; set; }

    }
}
