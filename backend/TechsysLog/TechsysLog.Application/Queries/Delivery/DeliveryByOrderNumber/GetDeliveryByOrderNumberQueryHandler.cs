using MediatR;
using TechsysLog.Application.Dtos.Deliveries;
using TechsysLog.Domain.Interfaces.Persistence.OrdersDelivery;

namespace TechsysLog.Application.Queries.Delivery.DeliveryByOrderNumber
{
    public class GetDeliveryByOrderNumberQueryHandler : IRequestHandler<GetDeliveryByOrderNumberQuery, GetDeliveryResponseDto>
    {
        private readonly IOrderDeliveryRepository _orderDeliveryRepository;

        public GetDeliveryByOrderNumberQueryHandler(IOrderDeliveryRepository orderDeliveryRepository)
        {
            _orderDeliveryRepository = orderDeliveryRepository;
        }

        public async Task<GetDeliveryResponseDto> Handle(GetDeliveryByOrderNumberQuery querie, CancellationToken cancellationToken)
        {
            var orderDelivery = await _orderDeliveryRepository.GetByOrderNumberAsync(querie.OrderNumber);

            if (orderDelivery is null)
            {
                return null;
            }

            var dto = GetDeliveryResponseDto.New(orderDelivery);
            return dto;
        }
    }
}
