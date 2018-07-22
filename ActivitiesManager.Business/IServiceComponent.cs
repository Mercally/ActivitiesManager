using ActivitiesManager.Data.Interfaces;
using ActivitiesManager.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Business
{
    public interface IServiceComponent
    {
        IActivitiesManagerProvider DbProvider { get; }

        List<Proyecto> ObtenerTodos();
    }
}
