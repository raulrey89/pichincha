using Pichincha.Domain.Base;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> Repository<T>() where T : Entity
        {
            return new RepositoryBase<T>(_dbContext);
        }
    }
}
