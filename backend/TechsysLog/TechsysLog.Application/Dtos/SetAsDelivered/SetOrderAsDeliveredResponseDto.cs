using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Enums;

namespace TechsysLog.Application.Dtos.SetAsDelivered
{
    public record SetOrderAsDeliveredResponseDto
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public OrderStatusEnum Status { get; set; }
        public OrderDelivery? OrderDelivery { get; set; }

        public static SetOrderAsDeliveredResponseDto New(Order order, OrderDelivery orderDelivery)
        {
            return new SetOrderAsDeliveredResponseDto
            {
                Id = order.Id,
                Number = order.Number,
                Description = order.Description,
                Value = order.Value,
                Status = order.Status,
                OrderDelivery = orderDelivery   
            };
        }
    }
}
