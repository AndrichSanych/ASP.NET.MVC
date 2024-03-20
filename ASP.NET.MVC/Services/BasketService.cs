using ASP.NET.MVC.Helpers;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;
using System.Text.Json;

namespace ASP.NET.MVC.Services
{
    public class BasketService : IBasketService
    {
        const string key = "basket_items_key";
        private readonly IProductsService productsService;
        private readonly HttpContext httpContext;

        public BasketService(IProductsService productsService, IHttpContextAccessor contextAccessor)
        {
            this.productsService = productsService;
            httpContext = contextAccessor.HttpContext ?? throw new Exception();
        }

        private List<int> GetBasketItems()
        {
            //var value = httpContext.Session.GetString(key);
            //return value == null ? new() : JsonSerializer.Deserialize<List<int>>(value)?? new();
            return httpContext.Session.Get<List<int>>(key) ?? new();
        }

        private void SaveBasketItems(List<int> items)
        {
            httpContext.Session.SetString(key, JsonSerializer.Serialize(items));            
        }

        void IBasketService.AddProduct(int id)
        {
            var ids = GetBasketItems();

            ids.Add(id);

            SaveBasketItems(ids);
        }

        IEnumerable<ProductDto> IBasketService.GetProducts()
        {
            var ids = GetBasketItems();
            return productsService.Get(ids);
        }

        void IBasketService.Remove(int id)
        {
            var ids = GetBasketItems();
            ids.Remove(id);
              
            SaveBasketItems(ids);

        }

        public int GetCount()
        {
           return GetBasketItems().Count;
        }

        public bool isExist(int id)
        {
            return GetBasketItems().Contains(id);
        }
    }
}
