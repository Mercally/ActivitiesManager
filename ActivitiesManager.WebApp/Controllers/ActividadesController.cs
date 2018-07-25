using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActivitiesManager.WebApiRestFulClient;

namespace ActivitiesManager.WebApp.Controllers
{
    public class ActividadesController : Controller
    {
        public ActividadesController(IPublicServicesWebApi publicServicesWebApi)
        {
            PublicServicesWebApi = publicServicesWebApi;
        }

        public IPublicServicesWebApi PublicServicesWebApi { get; }

        public IActionResult Actividadcomponent(int id)
        {
            

            return View();
        }
    }
}