using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET.MVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopDbContext context;
        private readonly IMapper mapper;

        public ProductsController(ShopDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        private void LoadCategories()
        {
            //1: TempData[key] = value;
            //2: ViewBag.Key = value;
            var categories = mapper.Map<List<CategoryDto>>(context.Categories.ToList());
            ViewBag.Categories = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
        }
        public IActionResult Index()
        {
            var products = mapper.Map<List<ProductDto>>(context.Products.Include(x => x.Category).ToList());

            return View(products);
        }
        [HttpGet]
        public IActionResult Create()
        {
            LoadCategories();
            return View();
        }

        [HttpPost] 
        public IActionResult Create(ProductDto model)
        {
            if(!ModelState.IsValid) 
            {
                LoadCategories();
                return View();
            }
            context.Products.Add(mapper.Map<Product>(model)); 
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound();
            LoadCategories();
            return View(mapper.Map<ProductDto>(product));
        }

        [HttpPost]
        public IActionResult Edit(ProductDto model)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories();
                return View();
            }
            context.Products.Update(mapper.Map<Product>(model));
            context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id, string? returnUrl)
        {
            var product = context.Products.Find(id);
            if (product == null) return NotFound();   
            
            context.Entry(product).Reference(x=>x.Category).Load();
            //1
            //var dto = new ProductDto()
            //{
            //    Id = product.Id,
            //    CategoryId = product.CategoryId,
            //    Description = product.Description,
            //    Discount = product.Discount,
            //    ImageUrl = product.ImageUrl,
            //    InStock = product.InStock,
            //    Name = product.Name,
            //    Price = product.Price,
            //    CategoryName = product.Category.Name
            //};
            //2
            var dto = mapper.Map<ProductDto>(product);

            ViewBag.ReturnUrl = returnUrl;
            return View(dto);
            
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
