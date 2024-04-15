using ASP.NET.MVC.Models;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP.NET.MVC.Controllers
{
    public class HomeController : Controller
    {
        private List<UserTest> users = new();
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
           users.Add(new UserTest() { Id = 23, Login = "Blabla"});
           users.Add(new UserTest() { Id = 25, Login = "BlablaMax"});
            this.productsService = productsService;

        }

        public IActionResult Index()
        {
            return View(productsService.GetAll());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Users()
        {
            return View(users);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}