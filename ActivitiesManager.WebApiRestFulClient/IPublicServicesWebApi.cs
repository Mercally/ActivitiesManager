using ActivitiesManager.WebApiRestFulClient.Metadata.WebApi;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.WebApiRestFulClient
{
    public interface IPublicServicesWebApi
    {
        IProyectosControllerApi ProyectosControllerApi();
    }
}
