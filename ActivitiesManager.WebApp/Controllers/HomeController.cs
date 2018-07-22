using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActivitiesManager.WebApp.Models;
using ActivitiesManager.WebApiRestFulClient;

namespace ActivitiesManager.WebApp.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IPublicServicesWebApi publicServicesWebApi)
        {
            ServicesWebApi = publicServicesWebApi;
        }

        private IPublicServicesWebApi ServicesWebApi { get; }

        public IActionResult Index()
        {
            var Todo = ServicesWebApi.ProyectosControllerApi().ObtenerTodo();

            ViewData["Todo"] = Todo;

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
