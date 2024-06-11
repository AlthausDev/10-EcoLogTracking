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

        /// <summary>
        /// MÉTODO QUE ACCEDE A LA BASE DE DATOS Y OBTIENE TODOS LOS REGISTROS
        /// </summary>
        /// <returns>Lista _connectionString los registros obtenidos de la base de datos: EcoLogTrackingDB</returns>
        [HttpGet]
        public async Task<IEnumerable<Log>> GetAll()
        {
            return await _logService.GetAll();
        }


        /// <summary>
        /// MÉTODO QUE GUARDA EN LA BASE DE DATOS LOS REGISTROS RECIBIDOS
        /// </summary>
        /// <param name="log">NLog generado por los programas que implementan nuestro software</param>
        /// <returns>Boolean True si el guardado es satifactorio. False en caso contrario.</returns>
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
