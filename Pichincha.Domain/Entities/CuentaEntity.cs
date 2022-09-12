using Pichincha.Domain.Base;

namespace Pichincha.Domain.Entities
{
    public class CuentaEntity : Entity
    {
        public CuentaEntity()
        {
            Movimientos = new List<MovimientoEntity>();
        }
        public Guid Id { get; set; }
        public Guid IdCliente { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<MovimientoEntity> Movimientos { get; set; } = null!;
        public virtual ClienteEntity Cliente { get; set; } = null!;

    }
}
