using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProductManagementSystem.Core.Domain.Models;
using ProductManagementSystem.Core.Domain.Models.ViewModels;

namespace ProductManagementSystem.RepositoryAndServices.Service.CustomService.OrderItemService
{
    public interface IOrderItemService
    {
        Task<ICollection<OrderItemViewModel>> GetAllAsync();
        Task<OrderItemViewModel> GetByIDAsync(int orderItemId);
        Task<bool> InsertAsync(OrderItemInsertModel orderItemInsertModel);
        Task<bool> UpdateAsync(OrderItemUpdateModel orderItemUpdateModel);
        Task<bool> DeleteAsync(int orderItemId);
        Task<OrderItemViewModel> FindAsync(Expression<Func<OrderItem, bool>> match);
    }
}
