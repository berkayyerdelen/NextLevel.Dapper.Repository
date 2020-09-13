using System;
using Microsoft.Extensions.DependencyInjection;
using NextLevel.Dapper.Repository.AppConfig;
using NextLevel.Dapper.Repository.Service.Repository;

namespace NextLevel.Dapper.Repository.Service
{
    public static class ServiceRegisterExtensions
    {
        public static IServiceCollection AddDapperRepository(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            Connection.Value = connectionString;
            return services;
        }
    }
}