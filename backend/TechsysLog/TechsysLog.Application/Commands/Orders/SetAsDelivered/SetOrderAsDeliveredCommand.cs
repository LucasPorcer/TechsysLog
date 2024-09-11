using MediatR;
using TechsysLog.Application.Dtos.SetAsDelivered;

namespace TechsysLog.Application.Commands.Orders.UpdateOrder
{
    public class SetOrderAsDeliveredCommand : IRequest<SetOrderAsDeliveredResponseDto>
    {
        public int Number { get; set; }
    }
}
