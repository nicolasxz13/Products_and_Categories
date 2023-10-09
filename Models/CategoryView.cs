namespace Products_and_Categories.Models
{
    public class CategoryView
    {
        public Category? Category { get; set; } = new Category();
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
