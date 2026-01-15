using System.Threading.Tasks;
using BAL.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Auth;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	public class AuthController : ControllerBase
    {
		private readonly AuthBAL _authBAL;
		public AuthController(AuthBAL authBAL)
		{
			_authBAL = authBAL;
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login([FromBody] UserLogin user)
		{
			try
			{
				var token = await _authBAL.Login(user);
				return Ok(new { status = "Success", data = token});
			}
			catch(Exception ex)
			{
				throw new Exception($"Message: {ex.Message}, StackTrace: {ex.StackTrace}");
			}
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register([FromBody] RegisterUser user)
		{
			try
			{
				await _authBAL.Register(user);
				return Ok(new { status = "User created successfully" });
			}
			catch (Exception ex)
			{
				throw new Exception($"Message: {ex.Message}, StackTrace: {ex.StackTrace}");
			}

		}
	}
}
