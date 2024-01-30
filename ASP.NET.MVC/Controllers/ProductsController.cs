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
    }
}
