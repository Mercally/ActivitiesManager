using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ActivitiesManager.Shared.Models;
using ActivitiesManager.Business.Common;
using ActivitiesManager.Business;

namespace ActivitiesManager.WebApi.Areas.ServiceCommonApi.Controllers
{
    [Produces("application/json")]
    [Route("ServiceCommonApi/api/Proyectos")]
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
            var Todo = ServiceBusiness.ProyectoBusiness.ObtenerTodos();

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