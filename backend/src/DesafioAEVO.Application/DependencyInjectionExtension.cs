using DesafioAEVO.Application.Services.AutoMapper;
using DesafioAEVO.Application.UseCases.Order;
using DesafioAEVO.Application.UseCases.Product;
using Microsoft.Extensions.DependencyInjection;

namespace DesafioAEVO.Application
{
    public static class DependencyInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services)
        {
            AddAutoMapper(services);
            AddUseCases(services);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(opt => new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IGetAllProductsUseCase, GetAllProductsUseCase>();
            services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
            services.AddScoped<ICreateOrderUseCase, CreateOrderUseCase>();
            services.AddScoped<IGetAllOrdersUseCase, GetAllOrdersUseCase>();
            services.AddScoped<ISendOrderToQueueUseCase, SendOrderToQueueUseCase>();
        }
    }
}