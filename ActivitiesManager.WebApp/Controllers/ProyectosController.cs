using ActivitiesManager.Shared.Models;
using ActivitiesManager.WebApiRestFulClient;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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

        [HttpPost]
        public async Task<IActionResult> Create(Proyecto proyecto)
        {
            var Id = await PublicServicesWebApi
                .ProyectosControllerApi
                .CrearAsync(proyecto);

            if (Id.IsCorrect)
            {
                return RedirectToRoute("default");
            }
            else
            {
                return RedirectToAction("error", "home");
            }
        }
    }
}