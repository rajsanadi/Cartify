using Domain;
using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Core.Domain.Models
{
    public class User : BaseEntity
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string UserEmail { get; set; }

        [Required]
        public string UserPassword { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number.")]
        public string UserPhoneNo { get; set; }

       
        public ICollection<Cart> Carts { get; set; }

        
        public ICollection<Order> Orders { get; set; }
    }
}
