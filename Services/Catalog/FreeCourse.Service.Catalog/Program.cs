using FreeCourse.Service.Catalog.Models;
using FreeCourse.Service.Catalog.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Service.Catalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
           var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;
                var categoryService = serviceProvider.GetRequiredService<ICategoryService>();
                if (!categoryService.GetAllAsync().Result.Data.Any())
                {
                    categoryService.CreateAsync(new Dtos.CategoryDto() { Name = "SQL Kursları"}).Wait();
                    categoryService.CreateAsync(new Dtos.CategoryDto() { Name = "C# Kursları"}).Wait();
                }
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
