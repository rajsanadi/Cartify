using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ProductManagementSystem.Core.Domain.Models.ViewModels
{
    public class OrderItemViewModel
    {
        [JsonIgnore]
        public int OrderItemId { get; set; }
        
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Navigation Properties
        public string ProductName { get; set; }
    }

    public class OrderItemInsertModel:OrderItemViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }

    public class OrderItemUpdateModel : OrderItemInsertModel
    {
        [Required]
        public int OrderItemId { get; set; }
    }
}

