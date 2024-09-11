using MediatR;
using TechsysLog.Application.Dtos.Deliveries;

namespace TechsysLog.Application.Queries.Delivery.DeliveryByOrderNumber
{
    public class GetDeliveryByOrderNumberQuery : IRequest<GetDeliveryResponseDto>
    {
        public int OrderNumber { get; set; }
    }
}
