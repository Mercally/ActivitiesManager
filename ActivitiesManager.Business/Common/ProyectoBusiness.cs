using ActivitiesManager.Data.Context;
using ActivitiesManager.Data.Interfaces;
using ActivitiesManager.Shared.Models;
using System.Collections.Generic;

namespace ActivitiesManager.Business.Common
{
    public class ProyectoBusiness
    {
        public List<Proyecto> ObtenerTodos()
        {
            List<Proyecto> Todos = new List<Proyecto>();

            using (var DbProvider = new ActivitiesManagerProvider())
            {
                Todos = DbProvider
                        .Actmgr_Proyectos
                        .ObtenerTodos();
            }

            return Todos;
        }
    }
}
