using TechsysLog.Domain.Entities;

namespace TechsysLog.Domain.Interfaces.Jwt
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User? user, bool isDefault = false);
    }
}
