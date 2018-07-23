using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ActivitiesManager.Business;
using ActivitiesManager.Shared.Models;

namespace ActivitiesManager.WebApi.Areas.ServiceCommon.Controllers
{
    [Produces("application/json")]
    [Route("ServiceCommon/api/Actividades")]
    public class ActividadesController : Controller
    {
        public ActividadesController(IServiceBusiness serviceBusiness)
        {
            ServiceBusiness = serviceBusiness;
        }

        public IServiceBusiness ServiceBusiness { get; }

        [HttpGet]
        public IActionResult ObtenerTodos()
        {
            var Todo = ServiceBusiness
                .ActividadBusiness
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
        [Route("id/{id}")]
        public IActionResult ObtenerActividad(int id)
        {
            var Todo = ServiceBusiness
                .ActividadBusiness
                .ObtenerActividad(id);

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