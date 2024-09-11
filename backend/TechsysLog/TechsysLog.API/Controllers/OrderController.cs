using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Commands.Orders.CreateOrder;
using TechsysLog.Application.Commands.Orders.UpdateOrder;
using TechsysLog.Application.Dtos.CreateOrder;
using TechsysLog.Application.Dtos.SetAsDelivered;
using TechsysLog.Application.Queries.Orders.GetOrdersPaged;

namespace TechsysLog.API.Controllers
{
    [Authorize]
    public class OrderController : ApiControllerBase
    {
        public OrderController()
        {
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateOrderResponseDto))]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await Mediator.Send(command);
            return result is not null ? Created("", result) : BadRequest();
        }

        [HttpPost("delivery")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetOrderAsDeliveredResponseDto))]
        public async Task<IActionResult> DeliveryOrder([FromBody] SetOrderAsDeliveredCommand command)
        {
            var result = await Mediator.Send(command);
            return result is not null ? Ok(result) : BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateOrderResponseDto))]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersPagedQuerie querie)
        {
            var result = await Mediator.Send(querie);
            return result is not null ? Ok( result) : NotFound();
        }
    }
}
