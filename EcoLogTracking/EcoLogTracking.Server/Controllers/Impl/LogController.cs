using EcoLogTracking.Server.Controllers.Interfaces;
using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

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
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpPost]
        public bool PostLog(Log log)
        {
            _logger.Info($"{nameof(PostLog)}");
            _logger.Info("Entra en Post log");
            return _logService.PostLog(log);

            //try
            //{
            //    var logs = _logService.PostLog(log);
            //    return Ok(logs);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, "Error interno del servidor");
            //}
        }

    }
}
