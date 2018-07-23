using ActivitiesManager.WebApiRestFulClient.Services.WebApi;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ActivitiesManager.WebApiRestFulClient
{
    public class PublicServicesWebApi : IPublicServicesWebApi
    {
        public PublicServicesWebApi()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public ProyectosControllerApi ProyectosControllerApi
        {
            get
            {
                return new ProyectosControllerApi(Configuration);
            }
        }
    }
}
