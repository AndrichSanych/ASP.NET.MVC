using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    internal class BasketService : IBasketService
    {
        private readonly IMapper mapper;
        private readonly ShopDbContext context;
        public void AddProduct(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDto> GetProducts()
        {
            return mapper.Map<List<ProductDto>>(context.Products.Include(x => x.Category).ToList());
        }

        public void Remove(int id)
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
    }
}
