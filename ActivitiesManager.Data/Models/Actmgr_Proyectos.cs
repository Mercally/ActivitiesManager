using ActivitiesManager.Data.Context;
using ActivitiesManager.Data.Interfaces;
using ActivitiesManager.Data.Models.Query;
using ActivitiesManager.Shared.Models;
using System.Collections.Generic;

namespace ActivitiesManager.Data.Models
{
    public class Actmgr_Proyectos : BaseContext
    {
        public Actmgr_Proyectos(ICustomContext Contexto)
           : base(Contexto) { }

        public List<Proyecto> ObtenerTodos()
        {
            var Query = QueryBuilder.CreateQuery(typeof(Proyecto));
            return Ejecutar<List<Proyecto>>(Query);
        }

        public Proyecto ObtenerPorId(int id)
        {
            var Query = QueryBuilder.CreateQueryById(typeof(Proyecto), id);
            return Ejecutar<Proyecto>(Query);
        }
    }
}
