using Core.Domain.Models;
using Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models;
using RepositoryAndServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryAndServices.Service.CustomService.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> repository;

        public CategoryService(IRepository<Category> repository)
        {
            this.repository = repository;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var deleteuser = await repository.GetByIDAsync(Id);
            if (deleteuser != null)
            {
                await repository.DeleteAsync(deleteuser);
                return true;
            }
            return false;
        }

        public async Task<Category> FindAsync(Expression<Func<Category, bool>> match)
        {
            return await repository.FindAsync(match);
        }

        public async Task<ICollection<CategoryViewModel>> GetAllAsync()
        {
            var getall = await repository.GetAllAsync();

            return getall.Select(x => new CategoryViewModel
            {
                Id = x.Id,
                CategoryName = x.CategoryName
            }).ToList();
        }

        public async Task<CategoryViewModel> GetByIDAsync(int Id)
        {
            var getid = await repository.GetByIDAsync(Id);
            if (getid == null)
            {
                return null;
            }

            return new CategoryViewModel
            {
                Id = getid.Id,
                CategoryName = getid.CategoryName
            };
        }

        public async Task<bool> InsertAsync(CategoryInsertModel categoryInsertModel)
        {
            if (categoryInsertModel == null)
            {
                return false;
            }
            else
            {
                Category category = new()
                {

                    CategoryName = categoryInsertModel.CategoryName
                };
                return await repository.InsertAsync(category);
            }
        }

        public async Task<bool> UpdateAsync(CategoryUpdateModel categoryUpdateModel)
        {
            var productupdate = await repository.GetByIDAsync(categoryUpdateModel.Id);
            if (productupdate != null)
            {
                productupdate.CategoryName = categoryUpdateModel.CategoryName;

                return await repository.UpdateAsync(productupdate);
            }
            else
            {
                return false;
            }
        }
    }
}
