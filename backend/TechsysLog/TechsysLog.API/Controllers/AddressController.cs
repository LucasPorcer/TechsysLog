using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Commands.Address.GetAddress;
using TechsysLog.Application.WebServices.ViaCep.Responses;

namespace TechsysLog.API.Controllers
{
    [Authorize]
    public class AddressController : ApiControllerBase
    {
        public AddressController()
        {
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressResponseModel))]
        public async Task<IActionResult> GetAddress([FromQuery] GetAddressByCepCommand query)
        {
            var result = await Mediator.Send(query);
            return result is not null ? Ok(result) : NotFound();
        }
    }
}
