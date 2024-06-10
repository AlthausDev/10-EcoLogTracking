using EcoLogTracking.Server.Controllers.Interfaces;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Services.Impl;
using EcoLogTracking.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;
using System.Threading.Tasks;

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
        
        [HttpGet]
        public async Task<IEnumerable<Log>> GetAll()
        {
            return await _logService.GetAll();
        }

        [HttpPost]
        public IActionResult PostLog([FromBody] Log log)
        {
            try
            {             
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
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
