using Core.Domain.Models;
using Core.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Core.Domain.Models.ViewModels;
using ProductManagementSystem.RepositoryAndServices.Service.CustomService.OrderItemService;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("GetAllOrderItems")]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var orderItems = await _orderItemService.GetAllAsync();
            if (orderItems != null)
            {
                return Ok(orderItems);
            }
            return BadRequest("Unable to retrieve order items");
        }

        [HttpGet("{orderItemId}")]
        public async Task<IActionResult> GetOrderItemById(int orderItemId)
        {
            var orderItem = await _orderItemService.GetByIDAsync(orderItemId);
            if (orderItem != null)
            {
                return Ok(orderItem);
            }
            return BadRequest("Unable to retrieve the requested order item");
        }

        [HttpPost("AddOrderItem")]
        public async Task<IActionResult> AddOrderItem([FromBody] OrderItemInsertModel orderItemInsertModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _orderItemService.InsertAsync(orderItemInsertModel);
                if (result)
                {
                    return Ok("Order item added successfully");
                }
                else
                {
                    return BadRequest("Unable to add order item");
                }
            }
            return BadRequest("Model state is not valid");
        }

        [HttpPut("UpdateOrderItem")]
        public async Task<IActionResult> UpdateOrderItem([FromBody] OrderItemUpdateModel orderItemUpdateModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _orderItemService.UpdateAsync(orderItemUpdateModel);
                if (result)
                {
                    return Ok("Order item updated successfully");
                }
                else
                {
                    return BadRequest("Unable to update order item");
                }
            }
            return BadRequest("Model state is not valid");
        }

        [HttpDelete("DeleteOrderItem")]
        public async Task<IActionResult> DeleteOrderItem(int orderItemId)
        {
            bool result = await _orderItemService.DeleteAsync(orderItemId);
            if (result)
            {
                return Ok("Order item deleted successfully");
            }
            return BadRequest("Unable to delete order item");
        }
    }
}
