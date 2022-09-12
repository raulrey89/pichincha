﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Models.DTOs
{
    public class PersonaClienteReadDto : PersonaDto
    {
        public Guid IdCliente { get; set; }
        public Guid IdPersona { get; set; }
        public string Contrasena { get; set; } = null!;
        public bool? Estado { get; set; }
    }
}
