using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Commands.Orders.CreateOrder;
using TechsysLog.Application.Commands.Orders.UpdateOrder;
using TechsysLog.Application.Dtos.CreateOrder;
using TechsysLog.Application.Dtos.Pagination;
using TechsysLog.Application.Dtos.SetAsDelivered;
using TechsysLog.Application.Queries.Orders.GetOrdersPaged;
using TechsysLog.Domain.Entities;

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

        /// <summary>
        /// Neste método eu decidi realizar dentro do order pois seria um comportamento do pedido, esta action hoje está sendo disparada pelo front-end
        /// mas pode facilmente vir de uma messageria ou de outro serviço externo. Por conta disto tomei a decisao de deixar aqui.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("delivery")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SetOrderAsDeliveredResponseDto))]
        public async Task<IActionResult> DeliveryOrder([FromBody] SetOrderAsDeliveredCommand command)
        {
            var result = await Mediator.Send(command);
            return result is not null ? Ok(result) : BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PagedResult<Order>))]
        public async Task<IActionResult> GetOrders([FromQuery] GetOrdersPagedQuerie querie)
        {
            var result = await Mediator.Send(querie);
            return result is not null ? Ok( result) : NotFound();
        }
    }
}
