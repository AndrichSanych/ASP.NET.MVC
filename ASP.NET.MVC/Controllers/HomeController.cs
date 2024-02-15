using ASP.NET.MVC.Models;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ASP.NET.MVC.Controllers
{
    public class HomeController : Controller
    {
        private List<User> users = new();
        private readonly ShopDbContext context;

        public HomeController(ShopDbContext context)
        {
           users.Add(new User() { Id = 23, Login = "Blabla"});
           users.Add(new User() { Id = 25, Login = "BlablaMax"});
           this.context = context;
        }

        public IActionResult Index()
        {
            var products = context.Products.Include(x => x.Category).ToList();
            return View(products);
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