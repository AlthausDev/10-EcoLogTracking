using EcoLogTracking.Server.Controllers.Interfaces;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;

namespace EcoLogTracking.Server.Controllers.Impl
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LogController(ILogService logService)
        {            
            _logService = logService;
        }
        /*
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            _logger.Info($"{nameof(GetAll)}");
            _logger.Info("FUNCIONA!!!!!");

            try
            {
                var logs = await _logService.GetAll();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                return StatusCode(500, "Error interno del servidor");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> PostLog([FromBody] string log)
        {
            
            try
            {
                _logger.Info("Received log: {0}", log.Level);
                bool success = _logService.PostLog(log);
                if (success)
                {
                    return Ok();
                }
                else
                {
                    return StatusCode(500, "Error al insertar el log en la base de datos");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error processing log");
                return StatusCode(500, "Error interno del servidor");
            }
            
        }
        */

        [HttpPost]
        public async Task PostLog()
        {
            Debug.WriteLine("dfhdfgh");
            Console.WriteLine("WRTGWETGHE");
            
            
        }
    }
}
