using ASP.NET.MVC.Helpers;
using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.MVC.Controllers
{
    [Authorize (Roles = "Admin")]
    public class ProductsController : Controller
    {
        //private readonly ShopDbContext context;
        private readonly IProductsService productService;
        private readonly IMapper mapper;    

        public ProductsController(IProductsService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        private void LoadCategories()
        {
            //1: TempData[key] = value;
            //2: ViewBag.Key = value;
            var categories = productService.GetAllCategories();
            ViewBag.Categories = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
        }
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(productService.GetAll());
        }
        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost] 
        public IActionResult Create(CreateProductModel model)
        {
            if(!ModelState.IsValid) 
            {
                LoadCategories();
                return View();
            }

            productService.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = productService.Get(id);
            if (product == null) return NotFound();
            LoadCategories();
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(ProductDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View();
            }

            productService.Edit(model);
            return RedirectToAction(nameof(Index));
        }
        [AllowAnonymous]
        public IActionResult Details(int id, string? returnUrl)
        {
            var product = productService.Get(id);
            if (product == null) return NotFound();   
            
            ViewBag.ReturnUrl = returnUrl;
            return View(product);   
        }
        public IActionResult Delete(int id)
        {
           productService.Delete(id);
           return RedirectToAction(nameof(Index));
        }
    }
}
