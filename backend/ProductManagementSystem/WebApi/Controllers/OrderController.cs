using Core.Domain.Models;
using Core.Domain.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductManagementSystem.Core.Domain.Models.ViewModels;
using RepositoryAndServices.Service.CustomService.OrderService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllAsync();
            if (orders != null)
            {
                return Ok(orders);
            }
            return BadRequest("Unable to retrieve orders");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetByIDAsync(id);
            if (order != null)
            {
                return Ok(order);
            }
            return BadRequest("Unable to retrieve the requested order");
        }

        [HttpPost("AddOrder")]
        public async Task<IActionResult> AddOrder([FromBody] OrderInsertModel orderInsertModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _orderService.InsertAsync(orderInsertModel);
                if (result)
                {
                    return Ok("Order added successfully");
                }
                else
                {
                    return BadRequest("Unable to add order");
                }
            }
            return BadRequest("Model state is not valid");
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderUpdateModel orderUpdateModel)
        {
            if (ModelState.IsValid)
            {
                bool result = await _orderService.UpdateAsync(orderUpdateModel);
                if (result)
                {
                    return Ok("Order updated successfully");
                }
                else
                {
                    return BadRequest("Unable to update order");
                }
            }
            return BadRequest("Model state is not valid");
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool result = await _orderService.DeleteAsync(id);
            if (result)
            {
                return Ok("Order deleted successfully");
            }
            return BadRequest("Unable to delete order");
        }

        [HttpGet("GetProductsByOrderId/{orderId}")]
        public async Task<IActionResult> GetProductsByOrderId(int orderId)
        {
           
            var products = await _orderService.GetProductsByOrderIdAsync(orderId);

           
            if (products == null)
            {
                return NotFound($"Order with ID {orderId} not found.");
            }

            if (!products.Any())
            {
                
                return NotFound($"No products found for the order ID {orderId}.");
            }

            
            return Ok(products);
        }

    }
}
