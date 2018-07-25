using ActivitiesManager.Shared.Models;
using ActivitiesManager.Shared.Models.Web;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ActivitiesManager.WebApiRestFulClient.Services.WebApi
{
    public class ProyectosControllerApi
    {
        public ProyectosControllerApi(IConfiguration configuration)
        {
            this.ApiUri = configuration["AppSettings:ServicesUri:ServiceCommonApi:Proyectos"];
        }

        private string ApiUri { get; }

        /// <summary>
        /// Petición
        /// </summary>
        /// <returns></returns>
        public HttpResponse<List<Proyecto>> ObtenerTodo()
        {
            return RequestHelper.Request<List<Proyecto>>
                (
                Url: ApiUri,
                Method: HttpMethodEnum.Get,
                Data: null, // Opcional
                TimeOut: 1 // Opcional
                );
        }

        /// <summary>
        /// Petición async
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponse<List<Proyecto>>> ObtenerTodoAsync()
        {
            var Help = await RequestHelper.RequestAsync<List<Proyecto>>
                (
                Url: ApiUri,
                Method: HttpMethodEnum.Get,
                Data: null, // Opcional
                TimeOut: 1 // Opcional
                );

            return Help;
        }

    }
}
