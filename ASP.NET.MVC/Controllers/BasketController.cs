using ASP.NET.MVC.Helpers;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
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

        public IActionResult Index(string returnUrl)
        {           
            ViewBag.ReturnUrl = returnUrl;
            return View(basketService.GetProducts());
        }

        public IActionResult Add(int id, string returnUrl)
        {
            basketService.AddProduct(id);
            return Redirect(returnUrl);
        } 
        public IActionResult Remove(int id, string returnUrl)
        {
            basketService.Remove(id);
            return Redirect(returnUrl);
        }
    }
}
