using Pichincha.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Models.Request
{
    public class PersonaCliente : PersonaDto
    {
        public string Contrasena { get; set; } = null!;
        public bool? Estado { get; set; }
    }
}
