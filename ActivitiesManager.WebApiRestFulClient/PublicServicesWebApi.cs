using System;
using System.Collections.Generic;
using System.Text;
using ActivitiesManager.WebApiRestFulClient.Metadata.WebApi;
using ActivitiesManager.WebApiRestFulClient.Services.WebApi;
using Microsoft.Extensions.Configuration;

namespace ActivitiesManager.WebApiRestFulClient
{
    public class PublicServicesWebApi : IPublicServicesWebApi
    {
        public PublicServicesWebApi(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IProyectosControllerApi ProyectosControllerApi()
        {
            return new ProyectosControllerApi(Configuration);
        }
    }
}
