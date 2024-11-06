using ProductManagementSystem.Core.Domain.Models.ViewModels;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RepositoryAndServices.CustomService.ProductService;
using Core.Domain.Models.ViewModels;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await productService.GetAllAsync();
            if (products != null)
            {
                return Ok(products);
            }
            return BadRequest("Unable to retrieve products.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await productService.GetByIDAsync(id);
            if (product != null)
            {
                return Ok(product);
            }
            return BadRequest("Unable to retrieve the requested product.");
        }

        [HttpGet("GetProductByName/{productName}")]
        public async Task<IActionResult> GetProductByNameAsync(string productName)
        {
            var product = await productService.FindAsync(p => p.ProductName.ToLower() == productName.ToLower());

            
            if (product == null)
            {
                
                return NotFound(new { message = "Product not available" });
            }

            
            var productViewModel = new ProductViewModel
            {
                ProductId = product.Id,
                ProductName = product.ProductName,
                Price = product.Price,
                StockQuantity = product.StockQuantity,
                CategoryId = product.CategoryId,
            };

            return Ok(productViewModel); 
        }



        //[HttpGet("GetProductsByOrderId/{orderId}")]
        //public async Task<IActionResult> GetProductsByOrderId(int orderId)
        //{
        //    var products = await productService.GetProductsByOrderIdAsync(orderId);
        //    if (products != null && products.Any())
        //    {
        //        return Ok(products);
        //    }
        //    return NotFound("No products found for the specified order.");
        //}

        [HttpPost("AddProducts")]
        public async Task<IActionResult> AddProducts([FromBody] ProductInsertModel productInsertModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await productService.InsertAsync(productInsertModel); 

                if (result)
                {
                    return Ok("Product added successfully.");
                }
                else
                {
                    return BadRequest("Unable to add product.");
                }
            }

            return BadRequest("Model state is not valid.");
        }

        [HttpPut("UpdateProducts")]
        public async Task<IActionResult> UpdateProducts([FromBody] ProductUpdateModel productUpdateModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await productService.GetByIDAsync(productUpdateModel.ProductId);
            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            bool result = await productService.UpdateAsync(productUpdateModel); 

            if (result)
            {
                return Ok("Product updated successfully.");
            }
            else
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Unable to update product.");
            }
        }

        [HttpDelete("DeleteProducts/{id}")]
        public async Task<IActionResult> DeleteProducts(int id)
        {
            var existingProduct = await productService.GetByIDAsync(id);
            if (existingProduct != null)
            {
                bool result = await productService.DeleteAsync(id);
                if (result)
                {
                    return Ok("Product deleted successfully.");
                }
                else
                {
                    return BadRequest("Unable to delete product.");
                }
            }
            else
            {
                return NotFound("Product not found.");
            }
        }
    }
}
