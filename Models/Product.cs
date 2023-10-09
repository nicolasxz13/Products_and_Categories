using System.ComponentModel.DataAnnotations;

namespace Products_and_Categories.Models
{
    public class Product
    {
        [Key]
        public int Productid { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal? Price { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public List<Category> Categories{get;set;} = new List<Category>();

    }
}
