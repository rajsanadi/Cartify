using Core.Domain.Models;
using Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models;
using RepositoryAndServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RepositoryAndServices.Service.CustomService.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> orderRepository;
        private readonly IRepository<OrderItem> orderItemRepository;
        private readonly IRepository<Product> productRepository;

        public OrderService(IRepository<Order> orderRepository, IRepository<OrderItem> orderItemRepository, IRepository<Product> productRepository)
        {
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
            this.productRepository = productRepository;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var order = await orderRepository.GetByIDAsync(Id);
            if (order != null)
            {
                await orderRepository.DeleteAsync(order);
                return true;
            }
            return false;
        }

        public async Task<Order> FindAsync(Expression<Func<Order, bool>> match)
        {
            return await orderRepository.FindAsync(match);
        }

        public async Task<ICollection<OrderViewModel>> GetAllAsync()
        {
            var orders = await orderRepository.GetAllAsync();

            return orders.Select(o => new OrderViewModel
            {
                OrderId = o.Id,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount
                
            }).ToList();
        }

        public async Task<OrderViewModel> GetByIDAsync(int Id)
        {
            var order = await orderRepository.GetByIDAsync(Id);
            if (order == null)
            {
                return null;
            }

            return new OrderViewModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount
            };
        }

        public async Task<bool> InsertAsync(OrderInsertModel orderInsertModel)
        {
            if (orderInsertModel == null)
            {
                return false;
            }

            Order order = new()
            {
                OrderDate = orderInsertModel.OrderDate,
                TotalAmount = orderInsertModel.TotalAmount,
                UserId = orderInsertModel.UserId
            };

            return await orderRepository.InsertAsync(order);
        }

        public async Task<bool> UpdateAsync(OrderUpdateModel orderUpdateModel)
        {
            var order = await orderRepository.GetByIDAsync(orderUpdateModel.OrderId);
            if (order != null)
            {
                order.OrderDate = orderUpdateModel.OrderDate;
                order.TotalAmount = orderUpdateModel.TotalAmount;

                return await orderRepository.UpdateAsync(order);
            }
            return false;
        }

        public async Task<OrderViewModel> GetByOrderIdAsync(int orderId)
        {
            var order = await orderRepository.GetByIDAsync(orderId);
            if (order == null)
            {
                return null;
            }

            return new OrderViewModel
            {
                OrderId = order.Id,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount
            };
        }

        //public Task<ICollection<ProductViewModel>> GetProductsByOrderIdAsync(int orderId)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ICollection<ProductViewModel>> GetProductsByOrderIdAsync(int orderId)
        {
            var orderItem = await orderItemRepository.FindAsync(oi => oi.OrderId == orderId);

            if (orderItem == null)
            {
                return new List<ProductViewModel>();
            }

            var productId = orderItem.ProductId;
            var product = await productRepository.FindAsync(p => p.Id == productId);

            if (product == null)
            {
                return new List<ProductViewModel>();
            }

            return new List<ProductViewModel>
    {
        new ProductViewModel
        {
            ProductId = product.Id,
            ProductName = product.ProductName,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            CategoryId = product.CategoryId
        }
    };
        }

        
    }
}
