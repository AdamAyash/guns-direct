namespace WebAPIGateway.Services.Authentication.Models
{
    public class JwtModel
    {
        public string? Payload { get; set; }
        public string? UserId { get; set; }
        public string? TokenType { get; set; }
        public int? ExpiresIn { get; set; }
    }
}
