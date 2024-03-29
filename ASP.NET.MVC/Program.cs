using AutoMapper;
using BusinessLogic;
using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using DataAccess.Data;
using FluentValidation;
using DataAccess;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ASP.NET.MVC.Services;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            var builder = WebApplication.CreateBuilder(args);
            var connStr = builder.Configuration.GetConnectionString("LocalDb")!;

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext(connStr);

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<ShopDbContext>();

            builder.Services.AddAutoMapper();
            builder.Services.AddFluentValidators();

            builder.Services.AddCustomServices();
            builder.Services.AddScoped<IBasketService, BasketService>();

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromDays(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();

            app.MapRazorPages();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}