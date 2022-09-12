using Pichincha.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pichincha.Domain.Interfaces
{
    public interface IRepository<T> where T : Entity
    {
        Task<T?> GetAsync(Guid id);

        Task<T?> GetAsync(Expression<Func<T, bool>> expression);

        Task<List<T>> GetAllAsync();

        Task<T> AddAsync(T entity);

        Task AddRangeAsync(IList<T> value);

        Task<T> UpdateAsync(T entity);

        Task<bool> DeleteAsync(T entity);

        Task<List<T>> ListAsync(Expression<Func<T, bool>> expression);

        Task<int> SaveChangesAsync();
    }
}
