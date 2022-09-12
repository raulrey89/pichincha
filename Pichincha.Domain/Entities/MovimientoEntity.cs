using Pichincha.Domain.Base;

namespace Pichincha.Domain.Entities
{
    public class MovimientoEntity : Entity
    {
        public Guid Id { get; set; }
        public Guid IdCuenta { get; set; }
        public string? TipoMovimiento { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public bool? Estado { get; set; }
        public virtual CuentaEntity Cuenta { get; set; } = null!;
    }
}
