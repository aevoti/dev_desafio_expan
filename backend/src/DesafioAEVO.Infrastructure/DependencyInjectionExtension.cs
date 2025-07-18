using DesafioAEVO.Application.Abstractions;
using DesafioAEVO.Domain.Abstractions.Repositories;
using DesafioAEVO.Infrastructure.DataAccess;
using DesafioAEVO.Infrastructure.DataAccess.Repositories.Order;
using DesafioAEVO.Infrastructure.DataAccess.Repositories.Product;
using DesafioAEVO.Infrastructure.Extensions;
using DesafioAEVO.Infrastructure.Messaging;
using DesafioAEVO.Infrastructure.Messaging.Consumer;
using FluentMigrator.Runner;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DesafioAEVO.Infrastructure
{
    public static class DependencyInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddFluentMigrator(services, configuration);

            AddRepositories(services);
            AddMassTransitConfiguration(services, configuration);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.ConnectionString();

            services.AddDbContext<DesafioAEVOdbContext>(dbContextOptions =>
            {
                dbContextOptions.UseSqlServer(connectionString);
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<IEventPublisher, MassTransitEventPublisher>();
        }

        private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentMigratorCore().ConfigureRunner(runner =>
            {
                runner
                .AddSqlServer()
                .WithGlobalConnectionString(configuration.ConnectionString())
                .ScanIn(Assembly.Load("DesafioAEVO.Infrastructure")).For.All();
            });
        }

        private static void AddMassTransitConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var host = configuration["RabbitMQ:Host"] ?? "localhost";
            var username = configuration["RabbitMQ:Username"] ?? "guest";
            var password = configuration["RabbitMQ:Password"] ?? "guest";

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(host, "/", h =>
                    {
                        h.Username(username);
                        h.Password(password);
                    });

                    cfg.ReceiveEndpoint("order-created-queue", e =>
                    {
                        e.ConfigureConsumer<OrderConsumer>(context);
                        e.UseMessageRetry(r => r.Interval(3, TimeSpan.FromSeconds(60)));
                    });
                });
            });
        }
    }
}
