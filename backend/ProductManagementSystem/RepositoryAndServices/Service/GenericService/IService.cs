using Domain;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryAndServices.Service.GenericService
{
    public interface IService<T> where T : BaseEntity
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> GetByIDAsync(int id);
        Task<bool> InsertAsync(T entity);
        Task<bool> UpdateAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> match);
    }
}
