using Microsoft.EntityFrameworkCore;
using Products_and_Categories.Models;

namespace Products_and_Categories.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions options)
            : base(options) { }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products {get;set;}
    }
}
