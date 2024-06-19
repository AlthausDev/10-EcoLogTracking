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
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public ConfigurationController(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConfiguration(Configuration configuration)
        {
            try
            {
                return await configurationService.updateConfiguration(configuration) ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " || " + e.StackTrace);
            }

        }




        [HttpGet]
        public async Task<IActionResult> getConfiguration()
        {
            try
            {
                Configuration response = await configurationService.getConfiguration();
                
                if (response != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest("Ha ocurrido algún error durante la configuración.");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " || " + e.StackTrace);
            }

        }
    }
}
