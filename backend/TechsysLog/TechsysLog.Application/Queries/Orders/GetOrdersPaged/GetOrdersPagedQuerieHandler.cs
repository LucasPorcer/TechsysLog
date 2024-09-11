using MediatR;
using TechsysLog.Application.Dtos.Pagination;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces.Persistence.Orders;

namespace TechsysLog.Application.Queries.Orders.GetOrdersPaged
{
    public class GetOrdersPagedQuerieHandler : IRequestHandler<GetOrdersPagedQuerie, PagedResult<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersPagedQuerieHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<PagedResult<Order>> Handle(GetOrdersPagedQuerie querie, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetPagedAsync(querie.PageIndex, querie.PageSize);
            var ordersTotal = await _orderRepository.GetTotalCountAsync();

            if (orders is null)
            {
                return null;
            }

            var dto = new PagedResult<Order>(ordersTotal, orders);
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
