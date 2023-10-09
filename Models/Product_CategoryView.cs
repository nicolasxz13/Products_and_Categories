namespace Products_and_Categories.Models
{
    public class Product_CategoryView
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public Product Product { get; set; } = new Product();

        public int? CategoryID { get; set; }
    }
}
