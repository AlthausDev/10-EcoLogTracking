using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Services.Impl;
using EcoLogTracking.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NLog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcoLogTracking.Server.Controllers.Impl
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController: Controller
    {
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public readonly IUserService userService;
        private readonly IConfigurationSection jwtConfiguration;
        private string tokenString;

        public UserController(IUserService userService, IConfiguration Configuration)
        {
            this.userService = userService;
            jwtConfiguration = Configuration.GetSection("JWT");
        }



        ///https://localhost:7216/api/login
        [HttpPost("/api/login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByUsername(User user)
        {
            try
            {
                var login = userService.GetUserByUsername(user.UserName);

                if (login == null)
                {
                   return BadRequest("Error, el usuario no existe.");
                }
                if (!login.Password.Equals(user.Password))
                {
                   return BadRequest("Error, contraseña no válida.");
                }

                var tokenKey = Encoding.UTF8.GetBytes(jwtConfiguration.GetValue<string>("Key"));

                Claim[] claims = [];
               
                    claims = new Claim[] {
                    new Claim(ClaimTypes.Role, "admin"),
                    new Claim(ClaimTypes.Name, login.UserName),
                    new Claim(ClaimTypes.NameIdentifier, login.Id.ToString())
                };
                
                

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Expires = DateTime.UtcNow.AddHours(jwtConfiguration.GetValue<int>("ExpirationHours")),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature),
                    Subject = new ClaimsIdentity(claims),
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);
                tokenString = tokenHandler.WriteToken(token);
                
                
                return Ok();
            }
            catch
            {
                return BadRequest("Error.");
            }
        }
    }
}
