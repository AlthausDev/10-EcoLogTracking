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

        [HttpPut("/{time}")]
        public async Task<IActionResult> UpdateConfig(int time)
        {
            try
            {
                return await configurationService.updateConfig(time) ? Ok() : BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message + " || " + e.StackTrace);
            }

        }
    }
}
