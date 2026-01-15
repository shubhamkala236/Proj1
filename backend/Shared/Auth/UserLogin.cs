using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Shared.Auth
{
    public class UserLogin
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
    }
}
