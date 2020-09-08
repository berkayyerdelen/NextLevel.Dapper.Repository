using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NextLevel.Dapper.Repository.Service.Repository;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IRepository<Product, int> _productRepository;

        public WeatherForecastController(IRepository<Product,int> repository)
        {
            _productRepository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> GetAll()
        {
           var t= await _productRepository.GetAllAsync("select id from product");
           return t;
        }
    }
}
