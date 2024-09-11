using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Dtos.Deliveries;
using TechsysLog.Application.Queries.Delivery.DeliveryByOrderNumber;

namespace TechsysLog.API.Controllers
{

    [Authorize]
    public class DeliveryController : ApiControllerBase
    {
        public DeliveryController()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetDeliveryResponseDto))]
        public async Task<IActionResult> GetDelivery([FromQuery] GetDeliveryByOrderNumberQuery query)
        {
            var result = await Mediator.Send(query);
            return result is not null ? Ok(result) : NotFound();
        }
    }
}
