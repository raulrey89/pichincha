using Pichincha.Domain.Base;

namespace Pichincha.Domain.Entities
{
    public class ClienteEntity : Entity
    {
        public Guid Id { get; set; }
        public Guid IdPersona { get; set; }
        public string Contrasena { get; set; } = null!;
        public bool? Estado { get; set; }
        public ICollection<CuentaEntity> Cuentas { get; set; }
        public  PersonaEntity Persona { get; set; } = null!;
    }
}
