using MediatR;
using Microsoft.AspNetCore.SignalR;
using TechsysLog.Application.Commands.Orders.UpdateOrder;
using TechsysLog.Application.Dtos.SetAsDelivered;
using TechsysLog.Application.Hubs.Orders;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces.Persistence.Orders;
using TechsysLog.Domain.Interfaces.Persistence.OrdersDelivery;

namespace TechsysLog.Application.Commands.Orders.SetAsDelivered
{
    public class SetOrderAsDeliveredCommandHandler : IRequestHandler<SetOrderAsDeliveredCommand, SetOrderAsDeliveredResponseDto>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDeliveryRepository _orderDeliveryRepository;
        private readonly IHubContext<OrdersHub> _hubContext;

        public SetOrderAsDeliveredCommandHandler(IOrderRepository orderRepository, 
            IHubContext<OrdersHub> hubContext,
            IOrderDeliveryRepository orderDeliveryRepository)
        {
            _orderRepository = orderRepository;
            _orderDeliveryRepository = orderDeliveryRepository;
            _hubContext = hubContext;
        }

        public async Task<SetOrderAsDeliveredResponseDto> Handle(SetOrderAsDeliveredCommand command, CancellationToken cancellationToken)
        {
            var order = await GetOrderAsync(command.Number);

            order.SetAsDelivered();

            await _orderRepository.UpdateAsync(order.Id, order);

            var orderDeliveryCreated = await CreateOrderDelivery(order);

            await SendSignalRNotification();

            var dto = SetOrderAsDeliveredResponseDto.New(order, orderDeliveryCreated);

            return dto;
        }

        private async Task SendSignalRNotification()
        {
            await _hubContext.Clients.All.SendAsync("AtualizarPedidos");
        }

        private async Task<OrderDelivery> CreateOrderDelivery(Order order)
        {
            var orderDeliveryToCreate = new OrderDelivery(order.Id, order.Number);
            var orderDelivaryCreated = await _orderDeliveryRepository.CreateAsync(orderDeliveryToCreate);
            return orderDelivaryCreated;
        }

        private async Task<Order> GetOrderAsync(int orderNumber)
        {
            var order = await _orderRepository.GetByNumberAsync(orderNumber);

            if (order is null)
            {
                throw new Exception($"Order with number {orderNumber} not found");
            }

            return order;
        }
    }
}
