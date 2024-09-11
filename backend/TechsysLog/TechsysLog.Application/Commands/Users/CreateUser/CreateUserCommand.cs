using MediatR;
using TechsysLog.Application.Dtos.CreateUser;

namespace TechsysLog.Application.Commands.Users.CreateUser
{
    public class CreateUserCommand : IRequest<CreateUserResponseDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
