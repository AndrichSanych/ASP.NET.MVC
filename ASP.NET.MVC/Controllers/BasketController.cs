using ASP.NET.MVC.Helpers;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ASP.NET.MVC.Controllers
{
    public class BasketController : Controller
    {        
        private readonly IBasketService basketService;

        public BasketController(IBasketService basketService)
        {          
            this.basketService = basketService;
        }

        public IActionResult Index()
        {           
            return View(basketService.GetProducts());
        }

        public IActionResult Add(int id)
        {
            basketService.AddProduct(id);
            return RedirectToAction(nameof(Index));
        } 
        public IActionResult Remove(int id)
        {
            basketService.Remove(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
