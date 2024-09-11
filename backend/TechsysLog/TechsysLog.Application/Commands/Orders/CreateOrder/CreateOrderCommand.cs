using MediatR;
using TechsysLog.Application.Dtos.CreateOrder;
using TechsysLog.Domain.Entities;

namespace TechsysLog.Application.Commands.Orders.CreateOrder
{
    public class CreateOrderCommand : IRequest<CreateOrderResponseDto>
    { 
        public int Number { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public OrderAddress OrderAddress { get; set; }
    }
}
