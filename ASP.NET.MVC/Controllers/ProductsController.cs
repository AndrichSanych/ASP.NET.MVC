using ASP.NET.MVC.Data;
using ASP.NET.MVC.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopDbContext context;

        public ProductsController(ShopDbContext context)
        {
            this.context = context;
        }

        public void LoadCategories()
        {
            //1: TempData[key] = value;
            //2: ViewBag.Key = value;
            ViewBag.Categories = new SelectList(context.Categories.ToList(), nameof(Category.Id), nameof(Category.Name));
        }
        public IActionResult Index()
        {
            var products = context.Products.Include(x => x.Category).ToList();

            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost] 
        public IActionResult Create(Product model)
        {
            if(!ModelState.IsValid) 
            {
                LoadCategories();
                return View();
            }
            context.Products.Add(model); 
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id, string? returnUrl)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound();   
            
            context.Entry(product).Reference(x=>x.Category).Load();
            
            ViewBag.ReturnUrl = returnUrl;
            return View(product);
            
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
