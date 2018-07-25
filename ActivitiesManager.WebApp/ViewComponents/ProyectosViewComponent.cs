using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ActivitiesManager.WebApiRestFulClient;

namespace ActivitiesManager.WebApp.ViewComponents
{
    public class ProyectosViewComponent : ViewComponent
    {
        public ProyectosViewComponent(IPublicServicesWebApi publicServicesWebApi)
        {
            PublicServicesWebApi = publicServicesWebApi;
        }

        public IPublicServicesWebApi PublicServicesWebApi { get; }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var Todo = await PublicServicesWebApi
                .ProyectosControllerApi
                .ObtenerTodoAsync();

            if (Todo.IsCorrect)
            {
                return View(Todo.Value);
            }
            else
            {
                return View("~/Views/Shared/UserError.cshtml", Todo);
            }
        }
    }
}