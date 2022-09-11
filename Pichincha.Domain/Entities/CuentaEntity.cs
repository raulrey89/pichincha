using Pichincha.Domain.Base;

namespace Pichincha.Domain.Entities
{
    public class CuentaEntity : Entity<int>
    {
        public CuentaEntity()
        {
            Movimientos = new List<MovimientoEntity>();
        }
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public string NumeroCuenta { get; set; } = null!;
        public string TipoCuenta { get; set; } = null!;
        public decimal SaldoInicial { get; set; }
        public bool? Estado { get; set; }

        public virtual ICollection<MovimientoEntity> Movimientos { get; set; } = null!;
        public virtual ClienteEntity Cliente { get; set; } = null!;

    }
}
