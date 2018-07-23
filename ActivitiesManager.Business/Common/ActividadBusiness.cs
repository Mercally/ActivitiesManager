using ActivitiesManager.Data.Context;
using ActivitiesManager.Data.Interfaces;
using ActivitiesManager.Shared.Models;
using System.Collections.Generic;

namespace ActivitiesManager.Business.Common
{
    public class ActividadBusiness
    {
        public List<Actividad> ObtenerTodos()
        {
            List<Actividad> Todos = new List<Actividad>();

            using (var DbProvider = new ActivitiesManagerProvider())
            {
                Todos = DbProvider
                            .Actmgr_Actividades
                            .ObtenerTodos();
            }

            return Todos;
        }

        public Actividad ObtenerActividad(int id)
        {
            Actividad Actividad = new Actividad();

            using (var DbProvider = new ActivitiesManagerProvider())
            {
                Actividad = DbProvider
                            .Actmgr_Actividades
                            .ObtenerActividad(id);
            }

            return Actividad;
        }
    }
}
