using System;
using System.Collections.Generic;
using System.Text;
using ActivitiesManager.Data.Context;
using ActivitiesManager.Data.Interfaces;
using ActivitiesManager.Shared.Models;

namespace ActivitiesManager.Business.Common
{
    public class ProyectoBusiness : IServiceComponent
    {
        public ProyectoBusiness(IActivitiesManagerProvider activitiesManagerProvider)
        {
            DbProvider = activitiesManagerProvider;
        }

        public IActivitiesManagerProvider DbProvider { get; }

        public List<Proyecto> ObtenerTodos()
        {
            List<Proyecto> Todos = new List<Proyecto>();

                Todos = DbProvider
                    .Actmgr_Proyectos
                    .ObtenerTodos();

                Todos = DbProvider
                    .Actmgr_Proyectos
                    .ObtenerTodos();

                Todos = DbProvider
                    .Actmgr_Proyectos
                    .ObtenerTodos();

            return Todos;
        }
    }
}
