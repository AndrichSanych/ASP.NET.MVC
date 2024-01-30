﻿using ASP.NET.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASP.NET.MVC.Controllers
{
    public class HomeController : Controller
    {
        private List<User> users = new();
        public HomeController()
        {
           users.Add(new User() { Id = 23, Login = "Blabla"});
           users.Add(new User() { Id = 25, Login = "BlablaMax"});
        }

        public IActionResult Index()
        {
            return View();
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