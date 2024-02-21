using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<ProductDto> GetAll()
        {
            return mapper.Map<List<ProductDto>>(context.Products.Include(x => x.Category).ToList());
        }

        public IEnumerable<CategoryDto> GetAllCategories()
        {
            return mapper.Map<List<CategoryDto>>(context.Categories.ToList());
        }
    }
}
