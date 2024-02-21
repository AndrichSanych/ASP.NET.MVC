using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public static class ServiceExtensions
    {
       

        public static void AddDbContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ShopDbContext>(opts => opts.UseSqlServer(connectionString));
        }

       
    }
}
