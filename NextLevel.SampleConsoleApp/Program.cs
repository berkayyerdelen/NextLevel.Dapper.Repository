﻿using System;
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

            var k = service.GetService<IRepository<Product, int>>();
            var t = await k.GetByIdAsync("product","id",1);
            Console.WriteLine(t.id);
           
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
        public int id { get; set; }
        public string name { get; set; }
    }
}
