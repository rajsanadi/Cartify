using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Domain.Models;
using Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models;

namespace RepositoryAndServices.Service.CustomService.CategoryService
{
    public interface ICategoryService
    {
        Task<ICollection<CategoryViewModel>> GetAllAsync();

        Task<CategoryViewModel> GetByIDAsync(int Id);

        Task<bool> InsertAsync(CategoryInsertModel categoryInsertModel);

        Task<bool> UpdateAsync(CategoryUpdateModel categoryUpdateModel);

        Task<bool> DeleteAsync(int Id);

        Task<Category> FindAsync(Expression<Func<Category, bool>> match);
    }
}
