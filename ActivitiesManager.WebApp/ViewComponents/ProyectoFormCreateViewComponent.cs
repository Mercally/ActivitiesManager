using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActivitiesManager.Shared.Models;
using ActivitiesManager.WebApiRestFulClient;

namespace ActivitiesManager.WebApp.ViewComponents
{
    public class ProyectoFormCreateViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {

            return View(new Proyecto());
        }
    }
}