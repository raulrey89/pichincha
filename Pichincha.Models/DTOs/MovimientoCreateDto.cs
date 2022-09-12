using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Models.DTOs
{
    public class MovimientoCreateDto
    {
        public Guid IdCuenta { get; set; }
        public string? TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
    }
}
