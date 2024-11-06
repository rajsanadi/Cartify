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

namespace RepositoryAndServices.Service.CustomService.CartService
{
    public class CartService : ICartService
    {
        private readonly IRepository<Cart> cartRepository;
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Category> categoryRepository;

        public CartService(
            IRepository<Cart> cartRepository,
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository)
        {
            this.cartRepository = cartRepository;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var cart = await cartRepository.GetByIDAsync(Id);
            if (cart != null)
            {
                await cartRepository.DeleteAsync(cart);
                return true;
            }
            return false;
        }

        public async Task<Cart> FindAsync(Expression<Func<Cart, bool>> match)
        {
            return await cartRepository.FindAsync(match);
        }

        public async Task<ICollection<CartViewModel>> GetAllAsync()
        {
            var carts = await cartRepository.GetAllAsync();

            var productIds = carts.Select(c => c.ProductId).Distinct();
            var products = await productRepository.FindAsync(p => productIds.Contains(p.Id));

            return carts.Select(c => new CartViewModel
            {
                CartId = c.Id,
                ProductId = c.ProductId,
                Quantity = c.Quantity,
                UserId = c.UserId,
                //ProductName = products.FirstOrDefault(p => p.Id == c.ProductId)?.ProductName
            }).ToList();
        }

        public async Task<CartViewModel> GetByIDAsync(int Id)
        {
            var cart = await cartRepository.GetByIDAsync(Id);
            if (cart == null)
            {
                return null;
            }

            var product = await productRepository.GetByIDAsync(cart.ProductId);

            return new CartViewModel
            {
                CartId = cart.Id,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                UserId = cart.UserId,
                ProductName = product?.ProductName
            };
        }

        public async Task<bool> InsertAsync(CartInsertModel cartInsertModel)
        {
            if (cartInsertModel == null)
            {
                return false;
            }

            var cart = new Cart
            {
                ProductId = cartInsertModel.ProductId,
                Quantity = cartInsertModel.Quantity,
                UserId = cartInsertModel.UserId
            };

            return await cartRepository.InsertAsync(cart);
        }

        public async Task<bool> UpdateAsync(CartUpdateModel cartUpdateModel)
        {
            var cart = await cartRepository.GetByIDAsync(cartUpdateModel.CartId);
            if (cart != null)
            {
                cart.ProductId = cartUpdateModel.ProductId;
                cart.Quantity = cartUpdateModel.Quantity;
                cart.UserId = cartUpdateModel.UserId;

                return await cartRepository.UpdateAsync(cart);
            }
            return false;
        }

        public async Task<string> GetCategoryNameByCartIdAsync(int cartId)
        {
            var cart = await cartRepository.GetByIDAsync(cartId);
            if (cart == null)
            {
                return null;
            }

            var product = await productRepository.GetByIDAsync(cart.ProductId);
            if (product == null)
            {
                return null;
            }

            var category = await categoryRepository.GetByIDAsync(product.CategoryId);
            return category?.CategoryName;
        }
    }
}
