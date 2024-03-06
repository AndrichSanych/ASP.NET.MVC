using ASP.NET.MVC.Helpers;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ASP.NET.MVC.Controllers
{
    public class BasketController : Controller
    {
        const string key = "basket_items_key";
        private readonly IProductsService productsService;

        public BasketController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            var ids = HttpContext.Session.Get<List<int>>(key) ?? new();
            return View(productsService.Get(ids));
        }

        public IActionResult Add(int id)
        {

            var ids = HttpContext.Session.Get<List<int>>(key) ?? new();
  
            ids.Add(id);

            HttpContext.Session.SetString(key, JsonSerializer.Serialize(ids));

            return RedirectToAction(nameof(Index));
        } 
        public IActionResult Remove(int id)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
