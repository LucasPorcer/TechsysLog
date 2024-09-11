using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Commands.Auth;
using TechsysLog.Application.Dtos.Auth;

namespace TechsysLog.API.Controllers
{
    [AllowAnonymous]
    [Authorize]
    public class LoginController : ApiControllerBase
    {
        public LoginController()
        {
        }

        /// <summary>Get Bearer Token to Authenticate Internal Requests</summary>
        /// <response code="200">Success: Returns generated token.</response>
        /// <response code="400">Failure: Invalid request.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AuthResponseDto))]
        public async Task<IActionResult> Authenticate([FromBody] AuthCommand command)
        {
            var result = await Mediator.Send(command);

            return Ok(result);
        }
    }

}
