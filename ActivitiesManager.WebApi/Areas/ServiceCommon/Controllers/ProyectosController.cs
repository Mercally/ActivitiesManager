using ActivitiesManager.Business;
using Microsoft.AspNetCore.Mvc;

namespace ActivitiesManager.WebApi.Areas.ServiceCommon.Controllers
{
    [Produces("application/json")]
    [Route("ServiceCommon/api/Proyectos")]
    public class ProyectosController : Controller
    {
        public ProyectosController(IServiceBusiness serviceBusiness)
        {
            ServiceBusiness = serviceBusiness;
        }

        public IServiceBusiness ServiceBusiness { get; }

        [HttpGet]
        public IActionResult ObtenerTodo()
        {
            var Todo = ServiceBusiness
                .ProyectoBusiness
                .ObtenerTodos();

            if (Todo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Todo);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult ObtenerPorId(int id)
        {
            var Todo = ServiceBusiness
                .ProyectoBusiness
                .ObtenerPorId(id);

            if (Todo == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(Todo);
            }
        }
    }
}