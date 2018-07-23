using ActivitiesManager.Shared.Models;
using ActivitiesManager.WebApiRestFulClient;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ActivitiesManager.WebApp.Controllers
{
    public class ProyectosController : Controller
    {
        public ProyectosController(IPublicServicesWebApi publicServicesWebApi)
        {
            PublicServicesWebApi = publicServicesWebApi;
        }

        public IPublicServicesWebApi PublicServicesWebApi { get; }
        
        public IActionResult Index()
        {
            var Respuesta = PublicServicesWebApi
                .ProyectosControllerApi
                .ObtenerTodo();

            if (Respuesta.IsCorrect)
            {
                return View(Respuesta.Value);
            }
            else
            {
                return RedirectToAction("Error", "Inicio");
            }
        }
    }
}