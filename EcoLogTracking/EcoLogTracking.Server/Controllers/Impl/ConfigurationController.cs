using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Services.Impl;
using EcoLogTracking.Server.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace EcoLogTracking.Server.Controllers.Impl
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfigurationService configurationService;

        public ConfigurationController(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        /// <summary>
        /// Método HTTP PUT para actualizar la configuración.
        /// </summary>
        /// <param name="configuration">Objeto de configuración que se va a actualizar</param>
        /// <returns>Retorna un estado HTTP 200 si la actualización fue exitosa, de lo contrario, un estado HTTP 400.</returns>
        [HttpPut]
        public async Task<IActionResult> UpdateConfiguration(Configuration configuration)
        {
            try
            {
                return await configurationService.updateConfiguration(configuration) ? Ok() : BadRequest("No se pudo actualizar la configuración."); ;
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + " || " + e.StackTrace);
            }
        }


        /// <summary>
        /// Método HTTP GET para obtener la configuración actual.
        /// </summary>
        /// <returns>Retorna la configuración actual si existe, de lo contrario, un estado HTTP 404.</returns>
        [HttpGet]
        public async Task<IActionResult> getConfiguration()
        {
            try
            {
                Configuration response = await configurationService.getConfiguration();

                return response != null ? Ok(response) : NotFound("No se encontró la configuración.");
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message + " || " + e.StackTrace);
            }

        }
    }
}
