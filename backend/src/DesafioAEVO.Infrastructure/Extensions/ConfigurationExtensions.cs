using Microsoft.Extensions.Configuration;

namespace DesafioAEVO.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string ConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("ConnectionSQLServer")!;
        }

        public static string RabbitMqConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString("RabbitMq")!;
        }
    }
}
