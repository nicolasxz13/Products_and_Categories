using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products_and_Categories.Data;
using Products_and_Categories.Models;

namespace Products_and_Categories.Controllers
{
    public class CategoryC : Controller
    {
        private readonly ProductContext _context;

        public CategoryC(ProductContext context)
        {
            _context = context;
        }

        [HttpGet("categories")]
        public IActionResult Index()
        {
            CategoryView categoryView = new CategoryView();
            categoryView.Categories = _context.Categories.ToList();
            return View(categoryView);
        }

        [HttpPost("category/create")]
        public IActionResult Create([Bind(Prefix = "CreateCategory")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                CategoryView categoryView = new CategoryView();
                categoryView.Category = category;
                categoryView.Categories = _context.Categories.ToList();
                return View("Index", categoryView);
            }
        }

        [HttpGet("category/{id}")]
        public IActionResult Show(int id)
        {
            Category_productView category_ProductView = new Category_productView();
            var temp = _context.Categories
                .Include(a => a.Products)
                .FirstOrDefault(a => a.CategoryId == id);
            var productemp = _context.Products
                .Where(product => !product.Categories.Any(category => category.CategoryId == id))
                .ToList();
            if (temp != null && productemp != null)
            {
                category_ProductView.Products = productemp;
                category_ProductView.Category = temp;
                return View(category_ProductView);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost("category/{id}/add")]
        public IActionResult AddProduct(int id, int? ProductID)
        {
            var temp = _context.Categories.FirstOrDefault(a => a.CategoryId == id);
            if (temp == null)
            {
                return RedirectToAction("Index");
            }

            if (ProductID == null)
            {
                Category_productView category_ProductView1 = new Category_productView();
                var temp1 = _context.Categories
                    .Include(a => a.Products)
                    .FirstOrDefault(a => a.CategoryId == id);
                var productemp = _context.Products
                    .Where(
                        product => !product.Categories.Any(category => category.CategoryId == id)
                    )
                    .ToList();
                if (temp1 != null && productemp != null)
                {
                    category_ProductView1.Products = productemp;
                    category_ProductView1.Category = temp1;
                    ModelState.AddModelError("ProductID", "Pruduct is required!");
                    return View("Show", category_ProductView1);
                }
            }
            Category category = new Category();

            category = temp;
            Product? product1 = _context.Products.Find(ProductID);
            if (product1 != null)
            {
                category.Products.Add(product1);
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
