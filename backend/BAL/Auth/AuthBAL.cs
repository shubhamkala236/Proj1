using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth;

namespace BAL.Auth
{
    public class AuthBAL
    {
        private readonly IConfiguration _configuration;
        public AuthBAL(IConfiguration configuration)
        {
			_configuration = configuration;

		}
		public async Task<string> Login(UserLogin user)
        {
            try
            {
				if (user.UserEmail == "admin@gmail.com" && user.Password == "password")
				{
					var token = GenerateJwtToken(user.UserEmail);
					return token;
				}

				throw new UnauthorizedAccessException("Invalid username or password.");
			}
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
			}
        }

		private string GenerateJwtToken(string userEmail)
		{
			var secret = _configuration["JWT_SECRET"];
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Email, userEmail),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

			var token = new JwtSecurityToken(
				claims: claims,
				expires: DateTime.Now.AddMinutes(30),
				signingCredentials: creds);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
