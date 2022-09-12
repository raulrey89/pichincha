using Microsoft.EntityFrameworkCore;
using Pichincha.Domain.Entities;
using Pichincha.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Infrastructure.Database
{

    public class AppDbContext : DbContext
    {
        #region Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClienteEntity>()
                .HasOne(p => p.Persona)
                .WithMany(b => b.Cliente)
                .HasForeignKey(p => p.IdPersona);

            modelBuilder.Entity<CuentaEntity>()
                .HasOne(p => p.Cliente)
                .WithMany(b => b.Cuentas)
                .HasForeignKey(p => p.IdCliente);

            modelBuilder.Entity<MovimientoEntity>()
                .HasOne(p => p.Cuenta)
                .WithMany(b => b.Movimientos)
                .HasForeignKey(p => p.IdCuenta);

            base.OnModelCreating(modelBuilder);
        }

        #region DbSets

        public DbSet<CuentaEntity> Cuenta { get; set; }
        public DbSet<MovimientoEntity> Movimiento { get; set; }
        public DbSet<ClienteEntity> Cliente { get; set; }
        public DbSet<PersonaEntity> Persona { get; set; }

        #endregion

    }
}
