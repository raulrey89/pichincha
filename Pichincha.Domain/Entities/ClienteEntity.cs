using Pichincha.Domain.Base;

namespace Pichincha.Domain.Entities
{
    public class ClienteEntity : Persona
    {
        public ClienteEntity()
        {
            Cuentas = new List<CuentaEntity>();
        }
        public string Contrasena { get; set; } = null!;
        public bool? Estado { get; set; }
        public virtual ICollection<CuentaEntity> Cuentas { get; set; }
    }
}
