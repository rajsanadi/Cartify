using Domain;
using ProductManagementSystem.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProductManagementSystem.Core.Domain.Models
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; }

        
        public ICollection<Product> Products { get; set; }
    }
}


