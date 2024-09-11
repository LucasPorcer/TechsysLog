using TechsysLog.Domain.Entities;

namespace TechsysLog.Application.Dtos.CreateUser
{
    public record CreateUserResponseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public static CreateUserResponseDto New(User user)
        {
            return new CreateUserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
            };
        }
    }
}
