using Domain;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Core.Domain.Models;
using ProductManagementSystem.Core.Domain.Models.ViewModels;
using RepositoryAndServices.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProductManagementSystem.RepositoryAndServices.Service.CustomService.OrderItemService
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IRepository<OrderItem> repository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Order> orderRepository;

        public OrderItemService(
            IRepository<OrderItem> repository,
            IRepository<Product> productRepository,
            IRepository<Order> orderRepository)
        {
            this.repository = repository;
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
        }

        public async Task<ICollection<OrderItemViewModel>> GetAllAsync()
        {
            var orderItems = await repository.GetAllAsync();

            return orderItems.Select(oi => new OrderItemViewModel
            {
                OrderItemId = oi.Id,
                OrderId = oi.OrderId,
                ProductId = oi.ProductId,
                Quantity = oi.Quantity,
                UnitPrice = oi.UnitPrice,
                ProductName = oi.Product != null ? oi.Product.ProductName : "Null" // Handle null Product
            }).ToList();
        }


        public async Task<OrderItemViewModel> GetByIDAsync(int orderItemId)
        {
            var orderItem = await repository.GetByIDAsync(orderItemId);

            if (orderItem == null)
            {
                return null;
            }

            return new OrderItemViewModel
            {
                OrderItemId = orderItem.Id,
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
               // ProductName = orderItem.Product.ProductName // Assuming Product navigation property is set
            };
        }

        public async Task<bool> InsertAsync(OrderItemInsertModel orderItemInsertModel)
        {
            if (orderItemInsertModel == null)
            {
                return false;
            }

            var orderItem = new OrderItem
            {
                OrderId = orderItemInsertModel.OrderId, 
                ProductId = orderItemInsertModel.ProductId,
                Quantity = orderItemInsertModel.Quantity,
                UnitPrice = orderItemInsertModel.UnitPrice
            };

            return await repository.InsertAsync(orderItem);
        }

        public async Task<bool> UpdateAsync(OrderItemUpdateModel orderItemUpdateModel)
        {
            var orderItem = await repository.GetByIDAsync(orderItemUpdateModel.OrderItemId);

            if (orderItem == null)
            {
                return false;
            }

            orderItem.OrderId = orderItemUpdateModel.OrderId;
            orderItem.ProductId = orderItemUpdateModel.ProductId;
            orderItem.Quantity = orderItemUpdateModel.Quantity;
            orderItem.UnitPrice = orderItemUpdateModel.UnitPrice;

            return await repository.UpdateAsync(orderItem);
        }

        public async Task<bool> DeleteAsync(int orderItemId)
        {
            var orderItem = await repository.GetByIDAsync(orderItemId);

            if (orderItem == null)
            {
                return false;
            }

            return await repository.DeleteAsync(orderItem);
        }

        public async Task<OrderItemViewModel> FindAsync(Expression<Func<OrderItem, bool>> match)
        {
            var orderItem = await repository.FindAsync(match);

            if (orderItem == null)
            {
                return null;
            }

            return new OrderItemViewModel
            {
                OrderItemId = orderItem.Id,
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                ProductName = orderItem.Product.ProductName 
            };
        }
    }
}
