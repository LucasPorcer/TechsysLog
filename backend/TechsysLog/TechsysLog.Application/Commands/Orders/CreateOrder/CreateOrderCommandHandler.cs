using MediatR;
using Microsoft.AspNetCore.SignalR;
using TechsysLog.Application.Dtos.CreateOrder;
using TechsysLog.Application.Hubs.Orders;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces.Persistence.Orders;

namespace TechsysLog.Application.Commands.Orders.CreateOrder
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CreateOrderResponseDto>
    {
        private readonly IHubContext<OrdersHub> _hubContext;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository,
            IHubContext<OrdersHub> hubContext)
        {
            _orderRepository = orderRepository;
            _hubContext = hubContext;
        }

        public async Task<CreateOrderResponseDto> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            await GetOrderByNumberAsync(command.Number, cancellationToken);

            var orderToCreate = new Order(command.Number, command.Description, command.Value, command.OrderAddress);
            orderToCreate.SetOpenStatus();

            await _orderRepository.CreateAsync(orderToCreate);

            await _hubContext.Clients.All.SendAsync("ListarPedidos");

            var dto = CreateOrderResponseDto.New(orderToCreate);

            return dto;
        }

        private async Task GetOrderByNumberAsync(int orderNumber, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByNumberAsync(orderNumber);
            
            if (order is not null)
            {
                throw new Exception("Order Already Exists");
            }
        }
    }
}
