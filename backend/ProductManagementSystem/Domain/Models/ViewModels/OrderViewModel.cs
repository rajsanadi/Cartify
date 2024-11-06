
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductManagementSystem.Core.Domain.Models.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }

        
        public List<OrderItemViewModel> OrderItems { get; set; }
    }

    public class OrderInsertModel
    {
        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public decimal TotalAmount { get; set; }

        public int UserId { get; set; }

       

        
        public List<OrderItemInsertModel> OrderItems { get; set; }
    }

    public class OrderUpdateModel : OrderInsertModel
    {
        [Required]
        public int OrderId { get; set; }
    }
}

