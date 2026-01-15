using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL;
using DAL.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth;

namespace BAL.Auth
{
    public class AuthBAL
    {
        private readonly IConfiguration _configuration;
		private readonly AppDbContext _context;
		public AuthBAL(IConfiguration configuration, AppDbContext context)
        {
			_configuration = configuration;
			_context = context;
		}

		public async Task<string> Login(UserLogin user)
        {
            try
            {
				// 1. Find user in DB
				var dbUser = await _context.Users
					.FirstOrDefaultAsync(u => u.Email == user.UserEmail);

				if (dbUser == null)
					throw new UnauthorizedAccessException("Invalid username or password.");

				// 2️. Verify password
				bool isValid = BCrypt.Net.BCrypt.Verify(user.Password, dbUser.PasswordHash);
				if (!isValid)
					throw new UnauthorizedAccessException("Invalid username or password.");

				// 3️. Generate JWT
				var token = GenerateJwtToken(dbUser.Email);

				return token;
			}
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
			}
        }

		public async Task Register(RegisterUser user)
		{
			var exists = await _context.Users.AnyAsync(x => x.Email == user.Email);
			if (exists)
				throw new Exception("User already exists");

			var userData = new User
			{
				Email = user.Email,
				PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password)
			};

			_context.Users.Add(userData);
			await _context.SaveChangesAsync();
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
