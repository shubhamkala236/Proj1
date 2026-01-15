using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Shared.Auth
{
	public class JwtService
	{
		private readonly string _secret;

		public JwtService(IConfiguration configuration)
		{
			_secret = configuration["Jwt:Secret"]
				?? throw new Exception("JWT secret missing");
		}

		public string GenerateToken(string username)
		{
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				claims: new[]
				{
				new Claim(JwtRegisteredClaimNames.Sub, username),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
				},
				expires: DateTime.UtcNow.AddMinutes(30),
				signingCredentials: creds
			);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
