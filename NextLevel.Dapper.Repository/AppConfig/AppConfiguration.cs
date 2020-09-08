using System.IO;
using Microsoft.Extensions.Configuration;

namespace NextLevel.Dapper.Repository.AppConfig
{
    public static class AppConfiguration
    {
        public static readonly string _connectionString;

        static AppConfiguration()
        {
            var configurationBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            var root = configurationBuilder.Build();
            _connectionString = root.GetSection("ConnectionString").Value;
        }
        public static string ConnectionString => _connectionString;
    }
}