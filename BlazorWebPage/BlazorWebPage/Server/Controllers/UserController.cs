using BlazorWebPage.Server.Services.Interfaces;
using BlazorWebPage.Shared;
using BlazorWebPage.Shared.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BlazorWebPage.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> Logger;
        private readonly IUserService UserService;
        private readonly IConfiguration Configuration;

        public UserController(ILogger<UserController> logger, IUserService userService, IConfiguration configuration)
        {
            Logger = logger;
            UserService = userService;
            Configuration = configuration;
        }

        [HttpGet("admin")]
        [AllowAnonymous]
        public ActionResult<List<User>> GetAdmin()
        {
            List<User> users = UserService.GetAllAdmin().ToList();
            if (users == null || !users.Any())
            {
                return NoContent();
            }
            return Ok(users);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<User>> Get()
        {
            List<User> users = UserService.GetAll().ToList();
            if (users == null || !users.Any())
            {
                return NoContent();
            }
            return Ok(users);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<User> GetUser(int id)
        {
            User user = UserService.GetById(id);           

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {          
            string tokenString = BuildToken(user);
            return Ok(new TokenResponse{ Token = tokenString });
        }

        [HttpPost]      
        private string BuildToken(User user)
        {
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]));
            SigningCredentials credentials = new(securityKey, SecurityAlgorithms.HmacSha256);

            var principal = new ClaimsPrincipal(new ClaimsIdentity(null, "Basic"));
            var isAuthenticated = principal.Identity.IsAuthenticated;

            Claim[] claims =
            [
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimTypes.Name, user.UserName),
                new(ClaimTypes.Role, "user")
            ];

            JwtSecurityToken token = new(
                issuer: Configuration["JWT:Issuer"],
                audience: Configuration["JWT:Issuer"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.UserName) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("User data is null or invalid.");
            }

            UserService.Add(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut]
        public IActionResult Put([FromBody] User user)
        {
            if (user == null || user.Id <= 0)
            {
                return BadRequest("User data is invalid.");
            }

            User existingUser = UserService.GetById(user.Id);
            if (existingUser == null)
            {
                return NotFound();
            }

            UserService.Update(user);
            return NoContent();
        }

        //[HttpDelete("{Id}")]
        //public IActionResult Delete(int id)
        //{
        //    User user = UserService.GetById(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    UserService.Remove(id);
        //    return NoContent();
        //}

        [HttpDelete("/Delete/{Id}")]
        public IActionResult LogicDelete(int id)
        {
            User user = UserService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            UserService.LogicRemove(id);
            return NoContent();
        }
    }
}
