using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProductManagementSystem.Core.Domain.Models;
using ProductManagementSystem.Core.Domain.Models.ViewModels;

namespace RepositoryAndServices.Service.CustomService.CartService
{
    public interface ICartService
    {
        Task<ICollection<CartViewModel>> GetAllAsync();
        Task<CartViewModel> GetByIDAsync(int Id);
        Task<bool> InsertAsync(CartInsertModel cartInsertModel);
        Task<bool> UpdateAsync(CartUpdateModel cartUpdateModel);
        Task<bool> DeleteAsync(int Id);
        Task<Cart> FindAsync(Expression<Func<Cart, bool>> match);

        Task<string> GetCategoryNameByCartIdAsync(int cartId);
    }
}
