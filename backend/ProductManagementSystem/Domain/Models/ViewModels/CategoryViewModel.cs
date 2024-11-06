using ProductManagementSystem.Core.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Core.Domain.Models.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
        [JsonIgnore]
        public ICollection<Product> Products { get; set; }
    }

    public class CategoryInsertModel
    {
        [Required]
        public string CategoryName { get; set; }
    }

    public class CategoryUpdateModel : CategoryInsertModel
    {
        [Required]
        public int Id { get; set; }
    }
}
