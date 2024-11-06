using Core.Domain.Models;
using Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryAndServices.Service.CustomService.OrderService
{
    public interface IOrderService
    {
        Task<ICollection<OrderViewModel>> GetAllAsync();
        Task<OrderViewModel> GetByIDAsync(int Id);
        Task<bool> InsertAsync(OrderInsertModel orderInsertModel);
        Task<bool> UpdateAsync(OrderUpdateModel orderUpdateModel);
        Task<bool> DeleteAsync(int Id);
        Task<OrderViewModel> GetByOrderIdAsync(int orderId);
        Task<ICollection<ProductViewModel>> GetProductsByOrderIdAsync(int orderId);
        Task<Order> FindAsync(Expression<Func<Order, bool>> match);
    }
}
