using MediatR;
using TechsysLog.Application.Dtos.CreateUser;
using TechsysLog.Common.Extensions;
using TechsysLog.Domain.Entities;
using TechsysLog.Domain.Interfaces.Persistence.Users;

namespace TechsysLog.Application.Commands.Users.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, CreateUserResponseDto>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<CreateUserResponseDto> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            await GetByEmailAsync(command.Email, cancellationToken);

            var userToCreate = new User(command.Name, command.Email, command.Password.ToBase64());

            var userCreated = await _userRepository.CreateAsync(userToCreate);

            var dto = CreateUserResponseDto.New(userCreated);

            return dto;
        }

        private async Task GetByEmailAsync(string userEmail, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailAsync(userEmail);

            if (user is not null)
            {
                throw new Exception("User Already Exists");
            }
        }
    }
}
