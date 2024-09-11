using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Enums;

namespace TechsysLog.Application.Dtos.CreateOrder
{
    public record CreateOrderResponseDto
    {
        public string Id { get; set; }
        public int Number { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public OrderStatusEnum Status { get; set; }
        public OrderAddress OrderAddress { get; set; }

        public static CreateOrderResponseDto New(Order order)
        {
            return new CreateOrderResponseDto
            {
                Id = order.Id,
                Number = order.Number,
                Description = order.Description,
                Value = order.Value,
                Status = order.Status,
                OrderAddress = order.OrderAddress,
            };
        }
    }
}
