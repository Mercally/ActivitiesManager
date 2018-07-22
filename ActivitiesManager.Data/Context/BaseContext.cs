using ActivitiesManager.Data.Interfaces;
using ActivitiesManager.Data.Models;
using ActivitiesManager.Data.Models.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Data.Context
{
    public class BaseContext
    {
        public BaseContext(ICustomContext Contexto)
        {
            this.Contexto = Contexto;
        }

        private BaseContext() { }

        private ICustomContext Contexto { get; set; }

        public QueryResult Ejecutar(QueryBuilder Consulta)
        {
            QueryResult Resultado = new QueryResult();

            try
            {
                Resultado = this.Contexto.Ejecutar(Consulta);
            }
            catch (Exception ex)
            {
                Resultado.Excepcion = ex;
                this.Contexto.Excepciones(ex);
            }

            return Resultado;
        }
    }
}
