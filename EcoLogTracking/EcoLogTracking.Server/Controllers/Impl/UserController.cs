﻿using EcoLogTracking.Server.Models;
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
    public class UserController : Controller
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

        /// <summary>
        /// MÉTODO QUE OBTIENE DE LA BASE DE DATOS LOS DATOS DE UN USUARIO A PARTIR DE SU USERNAME AND PASSWORD
        /// </summary>
        /// <param name="user">Nombre de usuario introducido por teclado</param>
        /// <param name="pass">Contraseña introducida por teclado</param>
        /// <returns>Devuelve objeto usuario con nombre, contraseña e id del usuario (si existe)</returns>
        ///https://localhost:7216/api/login
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserByUsernameAndPass(User user)
        {
            try
            {
                var login = await userService.GetUserByUsername(user.UserName);

                if (login == null)
                {
                    return BadRequest("Error, el usuario no existe.");
                }
                if (!login.Password.Equals(user.Password))
                {
                    return BadRequest("Error, contraseña no válida.");
                }

                var tokenKey = Encoding.UTF8.GetBytes(jwtConfiguration.GetValue<string>("Key"));

                var claims = new Claim[]
                {
                    new(ClaimTypes.Role, "admin"),
                    new(ClaimTypes.Name, login.UserName),
                    new(ClaimTypes.NameIdentifier,login.Id.ToString())
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


                return Ok(tokenString);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " || " + e.StackTrace);
            }
        }

        /// <summary>
        /// MÉTODO PARA REGISTRAR UN NUEVO USUARIO
        /// </summary>
        /// <param name="user">Objeto usuario con los datos del nuevo usuario que se quiere registrar</param>
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: no afecta a ninguna tupla)</returns>
        /// https://localhost:7216/api/User
        [HttpPost]
        [AllowAnonymous]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> PostUser(User user)
        {
            try
            {
                return await userService.PostUser(user) ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " || " + e.StackTrace);
            }
        }

        /// <summary>
        /// MÉTODO QUE ELIMINA LA CUENTA DEL PROPIO USUARIO A PARTIR DE SU ID
        /// </summary>
        /// <param name="id">Id del usuario que está borrando su cuenta</param>
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: no afecta a ninguna tupla)</returns>
        /// https://localhost:7216/api/User
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                return await userService.DeleteUser(id) ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " || " + e.StackTrace);
            }
        }


        /// <summary>
        /// MÉTODO QUE ACTUALIZA LOS DATOS DEL PROPIO USUARIO
        /// </summary>
        /// <param name="user">Objeto usuario con mismo id( para poder filtrar la tabla) pero con los nuevos datos del usuario</param>
        /// <returns>bool (true: si la consulta afecta a alguna tupla; false: no afecta a ninguna tupla)</returns>
        /// https://localhost:7216/api/User
        [HttpPut]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            try
            {
                return await userService.UpdateUser(user) ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " || " + e.StackTrace);
            }
        }

    }
}