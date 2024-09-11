namespace TechsysLog.Application.Dtos.Auth
{
    public record AuthResponseDto
    {
        public string Jwt { get; set; }
        public string UserName { get; set; }
    }
}
