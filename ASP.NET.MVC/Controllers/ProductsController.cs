using ASP.NET.MVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopDbContext context;

        public ProductsController(ShopDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var products = context.Products.ToList();

            return View(products);
        }
        public IActionResult Details(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            else
            {
                return View(product);
            }
        }
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if(product == null) 
            {
                return NotFound();
            }
            else 
            {
                context.Products.Remove(product);
                context.SaveChanges();

                return RedirectToAction(nameof(Index));
                
            }
        }
    }
}
