using BlazorWebPage.Server.Services.Interfaces;
using BlazorWebPage.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlazorWebPage.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TareaController : ControllerBase
    {
        private readonly ILogger<TareaController> _logger;
        private readonly ITareaService TareaService;

        public TareaController(ILogger<TareaController> logger, ITareaService tareaService)
        {
            _logger = logger;
            TareaService = tareaService;
        }

        [HttpGet]
        public List<Tarea> Get()
        {
            return (List<Tarea>)TareaService.GetAll();
        }

        [HttpPost]
        public void Post(Tarea tarea)
        {
            TareaService.Add(tarea);
        }

        [HttpPut]
        public void Put(Tarea tarea)
        {
            TareaService.Update(tarea);
        }     

        [HttpDelete("/DelTask/{id}")]
        public void Delete(int id)
        {
            TareaService.Remove(id);
        }
    }
}
