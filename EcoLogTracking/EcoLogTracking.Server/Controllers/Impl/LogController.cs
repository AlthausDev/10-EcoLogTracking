using EcoLogTracking.Server.Controllers.Interfaces;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EcoLogTracking.Server.Controllers.Impl
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        
        public LogController(ILogService logService)
        {            
            _logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var logs = await _logService.GetAll();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }
   
    }
}
