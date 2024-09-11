using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechsysLog.Application.Commands.Users.CreateUser;
using TechsysLog.Application.Dtos.CreateUser;

namespace TechsysLog.API.Controllers
{
    public class UserController : ApiControllerBase
    {
        public UserController()
        {
        }

        /// <summary>
        /// Comando para possivel criacao de usuario e senha
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CreateUserResponseDto))]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            var result = await Mediator.Send(command);

            return result is not null ? Created("", result) : BadRequest();
        }
    }
}
