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

       
        [HttpPost]
        public async Task<IActionResult> Post(Log log)
        {
            try
            {
                bool result = await _logService.Add(log, null);
                return Ok(result);
            }
            catch (Exception ex)
            {             
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
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
   

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLog(int id, Log log)
        {
            try
            {
                if (id != log.Id)
                {
                    return BadRequest("");
                }

                var updatedLog = await _logService.Update(log, null);
                return Ok(updatedLog);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error interno del servidor");
            }
        }

        [AllowAnonymous]
        [HttpGet("count")]
        public Task<int> Count()
        {
            return _logService.Count();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            try
            {
                _logService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {    
                return StatusCode(500, "Error interno del servidor");
            }
        }
    }
}
