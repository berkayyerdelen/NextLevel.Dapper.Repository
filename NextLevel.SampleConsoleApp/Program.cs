using System;
using System.Collections;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NextLevel.Dapper.Repository.Service;
using NextLevel.Dapper.Repository.Service.Repository;

namespace NextLevel.SampleConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var service = RegisterService();

            var productService = service.GetService<IRepository<Product, int>>();
            await productService.UpdateAsync("product", new Product(){Id = 5, Name = "deneme"}, 5);

            Console.WriteLine("Hello World!");
        }

        static IServiceProvider RegisterService()
        {
            var collection = new ServiceCollection();
            collection.AddDapperRepository(
                "Data Source=DESKTOP-HS9IPOH;Initial Catalog=Product;Integrated Security=SSPI;");
            return collection.BuildServiceProvider();
        }
    }

    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
