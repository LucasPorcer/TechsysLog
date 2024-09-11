using MediatR;
using Microsoft.Extensions.Configuration;
using TechsysLog.Application.Dtos.Auth;
using TechsysLog.Common.Extensions;
using TechsysLog.Domain.Interfaces.Jwt;
using TechsysLog.Domain.Interfaces.Persistence.Users;

namespace TechsysLog.Application.Commands.Auth
{
    public class AuthCommandHandler : IRequestHandler<AuthCommand, AuthResponseDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IConfiguration _configuration;

        public AuthCommandHandler(IUserRepository userRepository, IJwtTokenGenerator jwtTokenGenerator,
            IConfiguration configuration)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
            _configuration = configuration;
        }

        public async Task<AuthResponseDto> Handle(AuthCommand command, CancellationToken cancellationToken)
        {

            var token = string.Empty;
            var dto = new AuthResponseDto();

            var isDefaultLogin = ValidateDefaultLogin(command);

            if (!isDefaultLogin)
            {
                var user = await _userRepository.GetByEmailAndPasswordAsync(command.Email, command.Password.ToBase64());

                if (user == null)
                    throw new UnauthorizedAccessException("Invalid credentials");

                token = _jwtTokenGenerator.GenerateToken(user);

                dto.UserName = user.Name;
                dto.Jwt = token;
            }

            if (isDefaultLogin)
            {
                token = _jwtTokenGenerator.GenerateToken(null, true);

                dto.UserName = "admin";
                dto.Jwt = token;
            }

            return dto;
        }

        private bool ValidateDefaultLogin(AuthCommand command)
        {
            var defaultEmail = _configuration["Jwt:DefaultEmail"];
            var defaultPassword = _configuration["Jwt:DefaultPassword"];

            if (command.Email == defaultEmail && command.Password == defaultPassword)
            {
                return true;
            }

            return false;
        }
    }
}
