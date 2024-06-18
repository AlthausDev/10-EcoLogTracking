using EcoLogTracking.Server.Models;
using EcoLogTracking.Server.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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

        /// <summary>
        /// MÉTODO QUE ACCEDE A LA BASE DE DATOS Y OBTIENE TODOS LOS REGISTROS
        /// </summary>
        /// <returns>Lista _connectionString los registros obtenidos de la base de datos: EcoLogTrackingDB</returns>
        [HttpGet]
        [AllowAnonymous]
        // [Authorize(Roles = "admin")]
        public async Task<IEnumerable<Log>> GetAll()
        {
            return await _logService.GetAll();
        }


        /// <summary>
        /// MÉTODO QUE GUARDA EN LA BASE DE DATOS LOS REGISTROS RECIBIDOS
        /// </summary>
        /// <param name="log">NLog generado por los programas que implementan nuestro software</param>
        /// <returns>bool OK() si registro correcto/ BadRequest() si registro incorrecto</returns>
        [HttpPost]
        [AllowAnonymous]
        // [Authorize(Roles = "admin")]
        public async Task<IActionResult> PostLog([FromBody] Log log)
        {
            try
            {
                bool success = await _logService.PostLog(log);

                return success ? Ok() : StatusCode(500, "Error al insertar el log en la base de datos");
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        /// <summary>
        /// MÉTODO QUE FILTRA LOS REGISTROS EN LA BASE DE DATOS EN FUNCIÓN DE SU FECHA
        /// </summary>
        /// <param name="start">Fecha a partir de la cual se quieren obtener los registros</param>
        /// <param name="end">Fecha hasta la cual se quieren obtener los registros</param>
        /// <returns>bool OK(lista de logs filtrados) si filtrado correcto/ BadRequest() si filtrado incorrecto</returns>
        [HttpGet("/GetBetween/{start}/{end}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetLogsBetween(DateTime start, DateTime end)
        {
            var list = await _logService.GetLogsBetween(start, end);
            return list.IsNullOrEmpty() ? NotFound("No se han encontrado registros.") : Ok(list);
        }


        /// <summary>
        /// MÉTODO QUE ELIMINA LOS LOGS ANTERIORES AL NÚMERO DE DÍAS QUE RECIBE EL MÉTODO
        /// </summary>
        /// <param name="numDias">Número de días desde los que se quieren mantener los logs</param>
        /// <returns>bool OK() si borrado correcto/ BadRequest() si borrado incorrecto</returns>
        [HttpDelete("/{numDias}")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteLogsByDate(int numDias)
        {
            try
            {
                bool result = await _logService.DeleteLogsByDate(numDias);
                return result ? Ok("Registros borrados correctamente") : BadRequest("Error durante el borrado de los registros.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
