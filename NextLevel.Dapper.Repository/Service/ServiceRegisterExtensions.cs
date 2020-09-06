using Microsoft.Extensions.DependencyInjection;
using NextLevel.Dapper.Repository.Service.Repository;

namespace NextLevel.Dapper.Repository.Service
{
    public static class ServiceRegisterExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            return services;
        }
    }
}