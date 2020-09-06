using Microsoft.Extensions.Configuration;
using NextLevel.Dapper.Repository.Service.Repository;

namespace SampleRepository
{
    public class ProductRepository:Repository<Product,int>
    {
        public ProductRepository(IConfiguration configuration) : base(configuration)
        {
        }
        
    }
}