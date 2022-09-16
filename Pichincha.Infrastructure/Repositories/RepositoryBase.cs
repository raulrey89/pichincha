using Microsoft.EntityFrameworkCore;
using Pichincha.Domain.Base;
using Pichincha.Domain.Interfaces;
using Pichincha.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : Entity
    {
        protected readonly DbSet<T> DbSet;
        protected readonly AppDbContext DbContext;



        public RepositoryBase(AppDbContext dbContext)
        {
            DbSet = dbContext.Set<T>();
            DbContext = dbContext;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            return entity;
        }

        public async Task AddRangeAsync(IList<T> value)
        {
            await DbSet.AddRangeAsync(value);
        }

        public Task<bool> DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            return Task.FromResult(true);
        }

        public async Task<T?> GetAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> expression)
        {
            return await DbSet.FirstOrDefaultAsync(expression);
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> expression)
        {
            return await DbSet.Where(expression).ToListAsync();
        }

        public Task<T> UpdateAsync(T entity)
        {
            DbSet.Update(entity);
            return Task.FromResult(entity);
        }

        public Task<int> SaveChangesAsync()
        {
            return DbContext.SaveChangesAsync();
        }
    }
}
