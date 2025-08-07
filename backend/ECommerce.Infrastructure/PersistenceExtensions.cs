using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ECommerce.Infrastructure;

public static class PersistenceExtensions {
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString) {
        services.AddDbContext<ShopDbContext>(opt => opt.UseNpgsql(connectionString));
        return services;
    }
}
