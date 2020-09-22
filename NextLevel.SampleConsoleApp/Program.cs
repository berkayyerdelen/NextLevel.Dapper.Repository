using System;
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
            await productService.RemoveAsync("Product", 1);
            await productService.AddAsync("Product", new Product() {Name = "Dota"});
            Console.ReadKey();
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

    class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
