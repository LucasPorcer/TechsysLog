using TechsysLog.Domain.Entities;

namespace TechsysLog.Application.Dtos.Deliveries
{
    public record GetDeliveryResponseDto
    {
        public string Id { get; set; }
        public string OrderId { get; set; }
        public int OrderNumber { get; set; }
        public DateTime Date { get; set; }

        public static GetDeliveryResponseDto New(OrderDelivery orderDelivery)
        {
            return new GetDeliveryResponseDto { Id = orderDelivery.Id, OrderId = orderDelivery.OrderId, OrderNumber = orderDelivery.OrderNumber, Date = orderDelivery.Date };
        }
    }
}
