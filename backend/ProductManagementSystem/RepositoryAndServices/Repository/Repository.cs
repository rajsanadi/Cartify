using Domain;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Infrastructure.RepositoryAndServices.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryAndServices.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> dbset;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            this.dbset = context.Set<T>();
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            dbset.Remove(entity);
            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> match)
        {
            return await dbset.FirstOrDefaultAsync(match);
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await dbset.ToListAsync();
        }

        public async Task<T> GetByIDAsync(int id)
        {
            return await dbset.FindAsync(id);
        }

        public async Task<bool> InsertAsync(T entity)
        {
            dbset.Add(entity);
            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateAsync(T entity)
        {
            dbset.Update(entity);
            var result = await context.SaveChangesAsync();
            if (result > 0)
            {
                return true;
            }
            return false;
        }
    }
}
