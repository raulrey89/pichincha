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
    public interface IEfUnitOfWork : IUnitOfWork
    {
        /// <summary>
        ///     Returns a IDbSet instance for access to entities of the given type in the context,
        ///     the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <returns></returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
    }

    public class AppDbContext : DbContext, IEfUnitOfWork
    {
        #region Constructors

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        #region DbSets

        public DbSet<CuentaEntity> Cuenta { get; set; }
        public DbSet<MovimientoEntity> Movimiento { get; set; }
        public DbSet<ClienteEntity> Cliente { get; set; }
        public DbSet<PersonaEntity> Persona { get; set; }

        #endregion

        #region IUnitOfWork Implementation

        public void CommitChanges()
        {
            SaveChanges();
        }

        public void RollbackChanges()
        {
            ChangeTracker.Entries()
                .ToList()
                .ForEach(entry => entry.State = EntityState.Unchanged);
        }

        public DbSet<TEntity> CreateSet<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        #endregion
    }
}
