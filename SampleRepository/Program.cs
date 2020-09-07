using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NextLevel.Dapper.Repository.Service;
using NextLevel.Dapper.Repository.Service.Repository;

namespace SampleRepository
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var t = RegisterServices();
            var kk = t.GetService<ProductRepository>();
            var product = new Product()
            {
                Name = "test"
            };
            await kk.AddAsync("insert into products", product);
            Console.ReadKey();

        }

        public static IServiceProvider RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>();
            services.AddApplication();
            services.AddScoped<ProductRepository>();
            return services.BuildServiceProvider();
        }
    }
}
