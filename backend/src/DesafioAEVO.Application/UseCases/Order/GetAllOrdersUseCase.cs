using DesafioAEVO.Communication.Responses;
using DesafioAEVO.Domain.Abstractions.Repositories;

namespace DesafioAEVO.Application.UseCases.Order
{
    public class GetAllOrdersUseCase : IGetAllOrdersUseCase
    {
        private readonly IOrderRepository _orderRepository;
        public GetAllOrdersUseCase(
            IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IEnumerable<OrderResponse>> ExecuteAsync()
        {
            var orders = await _orderRepository.GetAllWithItemsAsync();

            return orders.Select(order => new OrderResponse
            {
                ID = order.ID,
                CreatedOn = order.CreatedOn,
                Status = order.Status.ToString(),
                Items = order.Items.Select(item => new OrderItemResponse
                {
                    ProductID = item.ProductID,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice,
                    ProductName = item.Product.Name
                }).ToList()
            }).ToList();
        }
    }
}
