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

        /// <summary>
        /// Este seria um comando que funciona como uma casa, sei que poderia ser feito a chamada para o ViaCEP diretamente do fornt-end, porém o acredito
        /// que o tratamento de resiliencia de chamadas externas no backend acaba ficando mais facil.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AddressResponseModel))]
        public async Task<IActionResult> GetAddress([FromQuery] GetAddressByCepCommand query)
        {
            var result = await Mediator.Send(query);
            return result is not null ? Ok(result) : NotFound();
        }
    }
}
