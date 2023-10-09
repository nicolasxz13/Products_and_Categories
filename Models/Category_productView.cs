namespace Products_and_Categories.Models
{
    public class Category_productView
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public Category Category { get; set; } = new Category();

        public int? ProductID { get; set; }
    }
}
