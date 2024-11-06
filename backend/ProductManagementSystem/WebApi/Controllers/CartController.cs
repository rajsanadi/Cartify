using Core.Domain.Models;
using Core.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Core.Domain.Models.ViewModels;
using RepositoryAndServices.Service.CustomService.CartService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet("GetAllCarts")]
        public async Task<IActionResult> GetAllCarts()
        {
            var carts = await _cartService.GetAllAsync();
            if (carts != null)
            {
                return Ok(carts);
            }
            return BadRequest("Unable to retrieve carts");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCartById(int id)
        {
            var cart = await _cartService.GetByIDAsync(id);
            if (cart != null)
            {
                return Ok(cart);
            }
            return BadRequest("Unable to retrieve the requested cart");
        }

        [HttpPost("AddCart")]
        public async Task<IActionResult> AddCart([FromBody] CartInsertModel cartInsertModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _cartService.InsertAsync(cartInsertModel);
                if (result)
                {
                    return Ok("Cart item added successfully");
                }
                else
                {
                    return BadRequest("Unable to add cart item");
                }
            }
            return BadRequest("Model state is not valid");
        }

        [HttpPut("UpdateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] CartUpdateModel cartUpdateModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _cartService.UpdateAsync(cartUpdateModel);
                if (result)
                {
                    return Ok("Cart item updated successfully");
                }
                else
                {
                    return BadRequest("Unable to update cart item");
                }
            }
            return BadRequest("Model state is not valid");
        }

        [HttpDelete("DeleteCart")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            bool result = await _cartService.DeleteAsync(id);
            if (result)
            {
                return Ok("Cart item deleted successfully");
            }
            return BadRequest("Unable to delete cart item");
        }

        [HttpGet("GetCategoryNameByCartId/{cartId}")]
        public async Task<IActionResult> GetCategoryNameByCartId(int cartId)
        {
            var categoryName = await _cartService.GetCategoryNameByCartIdAsync(cartId);
            if (categoryName != null)
            {
                return Ok(categoryName);
            }
            return NotFound("Category not found for the given cart ID");
        }
    }
}
