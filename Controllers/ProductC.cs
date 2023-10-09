using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_and_Categories.Data;
using Products_and_Categories.Models;

namespace Products_and_Categories.Controllers
{
    public class ProductC : Controller
    {
        private readonly ProductContext _context;

        public ProductC(ProductContext context)
        {
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ProductView productView = new ProductView();
            productView.Products = _context.Products.ToList();
            return View(productView);
        }

        [HttpPost("product/create")]
        public IActionResult Create([Bind(Prefix = "CreateProduct")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ProductView productView = new ProductView();
                productView.Product = product;
                productView.Products = _context.Products.ToList();
                return View("Index", productView);
            }
        }

        [HttpGet("product/{id}")]
        public IActionResult Show(int id)
        {
            Product_CategoryView product_CategoryView = new Product_CategoryView();
            var temp = _context.Products
                .Include(a => a.Categories)
                .FirstOrDefault(a => a.Productid == id);
            var categoryTemp = _context.Categories
                .Where(category => !category.Products.Any(product => product.Productid == id))
                .ToList();
            if (temp != null && categoryTemp != null)
            {
                product_CategoryView.Categories = categoryTemp;
                product_CategoryView.Product = temp;
                return View(product_CategoryView);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost("product/{id}/add")]
        public IActionResult AddProduct(int id, int? CategoryID)
        {
            var temp = _context.Products.FirstOrDefault(a => a.Productid == id);
            if (temp == null)
            {
                return RedirectToAction("Index");
            }

            if (CategoryID == null)
            {
                Product_CategoryView product_CategoryView = new Product_CategoryView();
                var temp1 = _context.Products
                    .Include(a => a.Categories)
                    .FirstOrDefault(a => a.Productid == id);
                var categorytemp = _context.Categories
                    .Where(Category => !Category.Products.Any(product => product.Productid == id))
                    .ToList();
                if (temp1 != null && categorytemp != null)
                {
                    product_CategoryView.Categories = categorytemp;
                    product_CategoryView.Product = temp1;
                    ModelState.AddModelError("ProductID", "Pruduct is required!");
                    return View("Show", product_CategoryView);
                }
            }
            Product product = new Product();

            product = temp;
            Category? category1 = _context.Categories.Find(CategoryID);
            if (category1 != null)
            {
                product.Categories.Add(category1);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
