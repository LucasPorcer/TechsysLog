using MediatR;
using TechsysLog.Application.Dtos.Auth;

namespace TechsysLog.Application.Commands.Auth
{
    public class AuthCommand : IRequest<AuthResponseDto>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
  
}
