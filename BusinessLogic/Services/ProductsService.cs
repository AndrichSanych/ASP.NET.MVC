using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IMapper mapper;
        private readonly ShopDbContext context;
        public ProductsService(IMapper mapper, ShopDbContext context) 
        {
            this.mapper = mapper;
            this.context = context;
        }

        public void Create(ProductDto product)
        {
            context.Products.Add(mapper.Map<Product>(product));
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return;
            }
            else
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }
        }

        public void Edit(ProductDto product)
        {
            context.Products.Update(mapper.Map<Product>(product));
            context.SaveChanges();
        }

        public ProductDto? Get(int id)
        {
            var item = (context.Products.Find(id));
            if (item == null) return null; 

            context.Entry(item).Reference(x => x.Category).Load();

            var dto = mapper.Map<ProductDto>(item);

            return dto;
        }

        public IEnumerable<ProductDto> Get(IEnumerable<int> ids)
        {
            return mapper.Map<List<ProductDto>>(context.Products
                .Include(x => x.Category)
                .Where(x=> ids.Contains(x.Id))
                .ToList());
        }

        public IEnumerable<ProductDto> GetAll()
        {
            return mapper.Map<List<ProductDto>>(context.Products.Include(x => x.Category).ToList());
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return mapper.Map<List<CategoryDto>>(context.Categories.ToList());
        }
    }

    internal class OrdersService : IOrdersService
    {
        private readonly IMapper mapper;
        private readonly ShopDbContext context;
        private readonly IBasketService basketService;

        public OrdersService(IMapper mapper, ShopDbContext context, IBasketService basketService) 
        {
            this.mapper = mapper;
            this.context = context;
            this.basketService = basketService;
        }
        public void Create(string userId)
        {
            var ids = basketService.GetProductsIds();
            var products = context.Products.Where(x => ids.Contains(x.Id)).ToList();
            var order = new Order()
            {
                Date = DateTime.Now,
                UserId = userId,
                Products = products,
                TotalPrice = products.Sum(x => x.Price),
            };

            context.Orders.Add(order);
            context.SaveChanges();            
        }

        public Task<IEnumerable<OrderDto>> GetAllByUser(string userId)
        {
            var items = context.Orders.Include(x => x.Products).Where(x => x.UserId == userId).ToList();
            return (Task<IEnumerable<OrderDto>>)mapper.Map<IEnumerable<OrderDto>>(items);
        }

        Task IOrdersService.Create(string userId)
        {
            throw new NotImplementedException();
        }

    }
}

