using AutoMapper;
using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Communication.Responses;
using DesafioAEVO.Domain.Entities;

namespace DesafioAEVO.Application.Services.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            RequestToDomain();
            DomainToResponse();
        }

        private void RequestToDomain()
        {
            CreateMap<RequestProductJson, Product>();
            CreateMap<RequestOrderItemJson, OrderItem>();
            CreateMap<RequestOrderJson, Order>();
        }

        private void DomainToResponse()
        {
            CreateMap<Product, ProductResponse>();

            CreateMap<OrderItem, OrderItemResponse>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name));

            CreateMap<Order, OrderResponse>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
        }
    }
}
