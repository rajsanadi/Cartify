using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementSystem.Core.Domain.Models.ViewModels
{
    public class CartViewModel  
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int UserId { get; set; }

        
        public string ProductName { get; set; }
    }

    public class CartInsertModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int UserId { get; set; }
    }

    public class CartUpdateModel : CartInsertModel
    {
        [Required]
        public int CartId { get; set; }
    }
}

