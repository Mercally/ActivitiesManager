using ActivitiesManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.WebApiRestFulClient.Metadata.WebApi
{
    public interface IProyectosControllerApi
    {
        List<Proyecto> ObtenerTodo();
    }
}
