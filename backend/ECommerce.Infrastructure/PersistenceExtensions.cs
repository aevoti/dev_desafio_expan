using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class PersistenceExtensions {
    public static IServiceCollection AddPersistence(this IServiceCollection services, string connectionString) {
        services.AddDbContext<ShopDbContext>(opt => opt.UseNpgsql(connectionString));
        return services;
    }
}
