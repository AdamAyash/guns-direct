namespace WebAPIGateway.Services.Authentication
{
    using Infrastructure.Users.DomainModels;
    using Microsoft.IdentityModel.Tokens;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;

    public class JWTService : IJwtTService
    {
        private readonly IConfiguration _configuration;

        public JWTService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public string GenerateSecurityToken(User userModel)
        {
            var jwtOptions = new JwtSecurityToken(
                        issuer: _configuration["JwtSettings:Issuer"],
                        audience: _configuration["JwtSettings:Audience"],
                        claims: GetClaims(userModel),
                        expires: DateTime.Now.AddMinutes(Convert.ToDouble(
                                _configuration["JwtSettings:ExpirationTimeInMinutes"])),
                        signingCredentials: GetSigningCredentials());

            string serializedJwtToken = new JwtSecurityTokenHandler().WriteToken(jwtOptions);

            return serializedJwtToken;
        }

        private SigningCredentials GetSigningCredentials()
        {
            var key = Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]);
            var secret = new SymmetricSecurityKey(key);
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Email) };
            return claims;
        }
    }
}
