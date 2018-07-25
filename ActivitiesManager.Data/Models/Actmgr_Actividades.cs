using ActivitiesManager.Data.Context;
using ActivitiesManager.Data.Interfaces;
using ActivitiesManager.Data.Models.Query;
using ActivitiesManager.Shared.Models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ActivitiesManager.Data.Models
{
    public class Actmgr_Actividades : BaseContext
    {
        public Actmgr_Actividades(ICustomContext context)
            : base(context)
        {
        }

        public List<Actividad> ObtenerTodos()
        {
            var Query = QueryBuilder.CreateQuery(typeof(Actividad)).Include("Proyecto");
            return Ejecutar<List<Actividad>>(Query);
        }

        public Actividad ObtenerActividad(int id)
        {
            var Query = new QueryBuilder(
                ConsultaCruda: "SELECT Id, Nombre FROM actmgr.Actividades WHERE Id = @id;",
                TypeQueryEnum: TypeQueryEnum.SELECT,
                Params: new List<SqlParameter>()
                    {
                        new SqlParameter("id", id)
                    }
                );
            return Ejecutar<Actividad>(Query);
        }

        public T ObtenerTodosPorProyectoId<T>(int id)
        {
            var Query = new QueryBuilder(
                ConsultaCruda: "SELECT Id, Nombre FROM actmgr.Actividades WHERE ProyectoId = @id;",
                TypeQueryEnum: TypeQueryEnum.SELECT,
                Params: new List<SqlParameter>()
                    {
                        new SqlParameter("id", id)
                    }
                );

            return Ejecutar<T>(Query);
        }
    }
}
