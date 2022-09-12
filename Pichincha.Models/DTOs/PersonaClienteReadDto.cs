using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Models.DTOs
{
    public class PersonaClienteReadDto : PersonaDto
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Identificacion { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public string Contrasena { get; set; } = null!;
        public bool? Estado { get; set; }
    }
}
