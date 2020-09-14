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
           //var k= service.GetService(typeof(IRepository<,>), typeof(Repository<,>));
           var p = service.GetRequiredService<IRepository<Product, int>>();
            var t = service.GetService<Repository<Product, int>>();
            await t.AddAsync("insert into products", new Product() { Id = 3, Name = "TestMAte" });
            Console.WriteLine("Hello World!");
        }

        static IServiceProvider RegisterService()
        {
            var collection = new ServiceCollection();
            collection.AddDapperRepository(
                "Data Source=DESKTOP-HS9IPOH;Initial Catalog=Product;Integrated Security=SSPI;");
            return collection.BuildServiceProvider(true);
        }
    }

    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
