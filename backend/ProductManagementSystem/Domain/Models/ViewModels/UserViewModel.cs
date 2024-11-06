using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProductManagementSystem.Core.Domain.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string UserEmail { get; set; }

        public string UserPhoneNo { get; set; }

        
        [JsonIgnore]
        public ICollection<CartViewModel> Carts { get; set; } = new List<CartViewModel>();
        [JsonIgnore]
        public ICollection<OrderViewModel> Orders { get; set; } = new List<OrderViewModel>();
    }

    public class UserInsertModel
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
    }

    public class UserUpdateModel : UserInsertModel
    {
        [Required]
        public int Id { get; set; }
    }

    public class LoginModel
    {
        [Required]
        public string UserName { get; set; } 

        [Required]
        public string UserPassword { get; set; }
    }
}
