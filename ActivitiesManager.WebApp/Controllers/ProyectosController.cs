using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActivitiesManager.WebApiRestFulClient;

namespace ActivitiesManager.WebApp.Controllers
{
    public class ProyectosController : Controller
    {
        public ProyectosController(IPublicServicesWebApi publicServicesWebApi)
        {
            PublicServicesWebApi = publicServicesWebApi;
        }

        public IPublicServicesWebApi PublicServicesWebApi { get; }

        [HttpPost]
        public IActionResult Details(int id)
        {
            var Detalle = PublicServicesWebApi
                .ProyectosControllerApi
                .ObtenerPorId(id);

            return View(Detalle.Value);
        }
    }
}