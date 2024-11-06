using Domain;
using RepositoryAndServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryAndServices.Service.GenericService
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public Task<bool> DeleteAsync(T entity)
        {
            return _repository.DeleteAsync(entity);
        }

        public Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return _repository.FindAsync(match);
        }

        public Task<ICollection<T>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<T> GetByIDAsync(int id)
        {
            return _repository.GetByIDAsync(id);
        }

        public Task<bool> InsertAsync(T entity)
        {
            return _repository.InsertAsync(entity);
        }

        public Task<bool> UpdateAsync(T entity)
        {
            return _repository.UpdateAsync(entity);
        }
    }
}
