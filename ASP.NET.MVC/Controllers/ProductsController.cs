using ASP.NET.MVC.Data;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private ShopDbContext context;
        public ProductsController()
        {
            context = new ShopDbContext();
        }
        public IActionResult Index()
        {
            var products = context.Products.ToList();

            return View(products);
        }
    }
}
