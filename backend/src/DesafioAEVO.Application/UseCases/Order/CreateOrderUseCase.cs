using AutoMapper;
using DesafioAEVO.Application.UseCases.Order.Validators;
using DesafioAEVO.Communication.Events;
using DesafioAEVO.Communication.Requests;
using DesafioAEVO.Communication.Responses;
using DesafioAEVO.Domain.Abstractions.Repositories;
using DesafioAEVO.Domain.Entities;
using DesafioAEVO.Exceptions.ExceptionsBase;
using DesafioAEVO.Exceptions.Resources;
using SendGrid.Helpers.Errors.Model;

namespace DesafioAEVO.Application.UseCases.Order
{
    public class CreateOrderUseCase : ICreateOrderUseCase
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISendOrderToQueueUseCase _sendOrderToQueueUseCase;

        public CreateOrderUseCase(
            IProductRepository productRepository,
            IOrderRepository orderRepository,
            IMapper mapper,
            IUnitOfWork unitOfWork,
            ISendOrderToQueueUseCase sendOrderToQueueUseCase)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _sendOrderToQueueUseCase = sendOrderToQueueUseCase;
        }

        public async Task<CreateOrderResponse> ExecuteAsync(RequestOrderJson request)
        {
            ValidateRequestAsync(request);

            var order = new Domain.Entities.Order();

            foreach (var item in request.Items!)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductID) ?? throw new NotFoundException(ResourceExceptions.PRODUCT_NOT_FOUND);

                var orderItem = new OrderItem
                {
                    ProductID = product.ID,
                    Quantity = item.Quantity,
                    UnitPrice = product.Price
                };

                order.Items.Add(orderItem);
            }


            await _orderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesOnDBAsync();

            var orderCreatedEvent = new OrderCreatedEvent
            {
                OrderId = order.ID,
                CreatedOn = order.CreatedOn,
                Status = order.Status.ToString(),
                Items = order.Items.Select(i => new OrderItemMessage
                {
                    ProductId = i.ProductID,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                }).ToList()
            };

            await _sendOrderToQueueUseCase.ExecuteAsync(orderCreatedEvent);

            var result = new CreateOrderResponse
            {
                OrderID = order.ID,
                Status = order.Status.ToString(),
                CreatedOn = order.CreatedOn
            };

            return result;
        }

        private void ValidateRequestAsync(RequestOrderJson request)
        {
            var validator = new NewOrderValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                var errorMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
