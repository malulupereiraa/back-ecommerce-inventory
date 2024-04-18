using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Web.Test.Entities;
using System;
using System.Globalization;

namespace Web.Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private IConfiguration _config;
        public LoginController(IConfiguration configuration)
        {
            _config = configuration;
        }

        private Login Autenticacao(Login login)
        {
            Login _user = null;
            if (login.Username == "admin" && login.Password == "12345")
            {
                _user = new Login { Username = "Malu Pereira" };
            }

            return _user;
        }

        private string GenerarTokenJWT(Login users)
        {
            var securitykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securitykey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"], _config["Jwt:Audience"], expires: DateTime.Now.AddHours(1), signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(Login user)
        {
            IActionResult response = Unauthorized();
            var user_ = Autenticacao(user);
            if (user_ != null)
            {
                var token = GenerarTokenJWT(user_);
                response = Ok(new { token = token });
            }
            return response;
        }
    }
}
