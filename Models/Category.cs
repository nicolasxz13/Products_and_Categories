using System.ComponentModel.DataAnnotations;

namespace Products_and_Categories.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }
}
