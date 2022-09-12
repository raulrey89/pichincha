using Pichincha.Domain.Base;

namespace Pichincha.Domain.Entities
{
    public abstract class Persona : Entity
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Genero { get; set; }
        public int? Edad { get; set; }
        public string? Identificacion { get; set; }
        public string Direccion { get; set; } = null!;
        public string Telefono { get; set; } = null!;

    }
}
