using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

using ProductManagementSystem.Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models;

namespace RepositoryAndServices.CustomService.ProductService
{
    public interface IProductService
    {
        Task<ICollection<ProductViewModel>> GetAllAsync();
        Task<ProductViewModel> GetByIDAsync(int Id);
        Task<bool> InsertAsync(ProductInsertModel productInsertModel);
        Task<bool> UpdateAsync(ProductUpdateModel productUpdateModel);
        Task<bool> DeleteAsync(int Id);
        Task<Product> FindAsync(Expression<Func<Product, bool>> match);

        
        Task<ICollection<ProductViewModel>> GetProductsByOrderIdAsync(int orderId);
        Task<ProductViewModel> GetProductByNameAsync(string productName);
        Task<string> GetCategoryNameByCartIdAsync(int cartId);
    }
}
