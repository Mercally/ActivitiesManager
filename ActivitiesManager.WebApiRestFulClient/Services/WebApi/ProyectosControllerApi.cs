using ActivitiesManager.Shared.Models;
using ActivitiesManager.WebApiRestFulClient.Metadata.WebApi;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.WebApiRestFulClient.Services.WebApi
{
    public class ProyectosControllerApi : IProyectosControllerApi
    {
        public ProyectosControllerApi(IConfiguration configuration)
        {
            this.Configuration = configuration;
            this.ApiUri = Configuration["ServiceCommonApiProyectosUri"];
        }

        private IConfiguration Configuration { get; }
        private string ApiUri { get; }

        /// <summary>
        /// Petición
        /// </summary>
        /// <returns></returns>
        public List<Proyecto> ObtenerTodo()
        {
            return HelperRequester.Request<List<Proyecto>>
                (
                Url: ApiUri,
                Method: HttpMethodEnum.Get,
                Data: null, // Opcional
                TimeOut: 1 // Opcional
                );
        }

    }
}
