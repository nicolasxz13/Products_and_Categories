namespace Products_and_Categories.Models{
    public class ProductView{
        public Product? Product {get;set;} = new Product();
        public List<Product> Products {get;set;} = new List<Product>();
    }
}