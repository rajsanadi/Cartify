using ProductManagementSystem.Core.Domain.Models.ViewModels;
using ProductManagementSystem.Core.Domain.Models;
using RepositoryAndServices.Repository;
using System.Linq.Expressions;

namespace RepositoryAndServices.CustomService.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<Cart> _cartRepository;

        public ProductService(
            IRepository<Product> productRepository,
            IRepository<Category> categoryRepository,
            IRepository<Order> orderRepository,
            IRepository<Cart> cartRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _orderRepository = orderRepository;
            _cartRepository = cartRepository;
        }

        public async Task<ICollection<ProductViewModel>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            return products.Select(p => new ProductViewModel
            {
                ProductId = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                CategoryName = categories.FirstOrDefault(c => c.Id == p.CategoryId)?.CategoryName
            }).ToList();
        }


        public async Task<ProductViewModel> GetByIDAsync(int id)
        {
            var product = await _productRepository.GetByIDAsync(id);
            if (product == null)
            {
                return null;
            }

            return new ProductViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = (await _categoryRepository.GetByIDAsync(product.CategoryId))?.CategoryName
            };
        }

        public async Task<bool> InsertAsync(ProductInsertModel productInsertModel)
        {
            if (productInsertModel == null)
            {
                return false;
            }

            var product = new Product
            {
                ProductName = productInsertModel.ProductName,
                Price = productInsertModel.Price,
                StockQuantity = productInsertModel.StockQuantity,
                CategoryId = productInsertModel.CategoryId,
                IsAvailable = productInsertModel.IsAvailable
            };

            return await _productRepository.InsertAsync(product);
        }

        public async Task<bool> UpdateAsync(ProductUpdateModel productUpdateModel)
        {
            var product = await _productRepository.GetByIDAsync(productUpdateModel.ProductId);
            if (product == null)
            {
                return false;
            }

            product.ProductName = productUpdateModel.ProductName;
            product.Price = productUpdateModel.Price;
            product.StockQuantity = productUpdateModel.StockQuantity;
            product.CategoryId = productUpdateModel.CategoryId;
            product.IsAvailable = productUpdateModel.IsAvailable;

            return await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _productRepository.GetByIDAsync(id);
            if (product != null)
            {
                return await _productRepository.DeleteAsync(product);
            }
            return false;
        }

        public async Task<Product> FindAsync(Expression<Func<Product, bool>> match)
        {
            return await _productRepository.FindAsync(match);
        }

        public async Task<ICollection<ProductViewModel>> GetProductsByOrderIdAsync(int orderId)
        {
            var order = await _orderRepository.GetByIDAsync(orderId);
            if (order == null)
            {
                return new List<ProductViewModel>();
            }

            var productIds = order.OrderItems.Select(oi => oi.ProductId).ToList();
            var products = await _productRepository.GetAllAsync();
            var categories = await _categoryRepository.GetAllAsync();

            return products.Where(p => productIds.Contains(p.Id)).Select(p => new ProductViewModel
            {
                ProductId = p.Id,
                ProductName = p.ProductName,
                Price = p.Price,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                CategoryName = categories.FirstOrDefault(c => c.Id == p.CategoryId)?.CategoryName
            }).ToList();
        }

        public async Task<ProductViewModel> GetProductByNameAsync(string productName)
        {
            var product = await _productRepository.FindAsync(p => p.ProductName.Equals(productName, StringComparison.OrdinalIgnoreCase));
            if (product == null)
            {
                return null;
            }

            return new ProductViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
                CategoryName = (await _categoryRepository.GetByIDAsync(product.CategoryId))?.CategoryName
            };
        }

        public async Task<string> GetCategoryNameByCartIdAsync(int cartId)
        {
            var cart = await _cartRepository.GetByIDAsync(cartId);
            if (cart == null)
            {
                return null;
            }

            var product = await _productRepository.GetByIDAsync(cart.ProductId);
            if (product == null)
            {
                return null;
            }

            var category = await _categoryRepository.GetByIDAsync(product.CategoryId);
            return category?.CategoryName;
        }
    }
}
