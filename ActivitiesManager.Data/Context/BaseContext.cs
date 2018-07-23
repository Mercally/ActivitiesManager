using ActivitiesManager.Data.Interfaces;
using ActivitiesManager.Data.Models;
using ActivitiesManager.Data.Models.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

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

        public T Ejecutar<T>(QueryBuilder Consulta)
        {
            QueryResult qResultado = new QueryResult();
            object Result = null;
            try
            {
                qResultado = this.Contexto.Ejecutar(Consulta);
                Result = qResultado.ConvertirResultado<T>();
            }
            catch (Exception ex)
            {
                qResultado.Excepcion = ex;
                this.Contexto.Excepciones(ex);
            }

            return (T)Result;
        }

        //private Object Resultado(QueryResult resultado)
        //{
        //    if (resultado.EsCorrecto)
        //    {
        //        throw new Exception("Error al ejecutar la consulta");
        //    }
        //    else
        //    {
        //        var Result = resultado.ConvertiresultadoUnico(Consulta.PrincipalType);

        //        if (Consulta.Includes.Length > 0)
        //        {
        //            var Propiedades = Consulta.PrincipalType.GetProperties().ToArray();
        //            foreach (var Include in Consulta.Includes)
        //            {
        //                PropertyInfo Property = null;
        //                foreach (var Prop in Propiedades)
        //                {
        //                    if (Prop.Name == Include)
        //                    {
        //                        Property = Prop;
        //                    }
        //                }

        //                Type PropType = Property.PropertyType;
        //                var SubQuery = QueryBuilder.CreateQuery(PropType);

        //                var SubProp = Ejecutar(SubQuery).ConvertiresultadoUnico(SubQuery.PrincipalType);

        //                if (Result != null)
        //                {
        //                    Property.SetValue(Result, SubProp);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}
