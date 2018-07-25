using ActivitiesManager.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ActivitiesManager.Data.Models.Query
{
    public class QueryResult : IQueryResult
    {
        internal int ResultadoTipoInsert { get; set; }
        internal bool ResultadoTipoUpdate { get; set; }
        internal bool ResultadoTipoDelete { get; set; }
        internal DataTable ResultadoTipoQuery { get; set; }
        public int CantidadCambios { get; set; }
        public System.Exception Excepcion { get; set; }
        
        public QueryBuilder Consulta { get; set; }

        public bool EsCorrecto
        {
            get
            {
                return Excepcion == null;
            }
        }

        /// <summary>
        /// Obtiene el resultado convertido al tipo especificado
        /// </summary>
        /// <typeparam name="T">Tipo de dato a retornar</typeparam>
        /// <returns>Lista de objetos de tipo especificado</returns>
        public T ConvertirResultado<T>()
        {
            if (!this.EsCorrecto)
            {
                return default(T);
            }

            Object Resultado = null;

            try
            {
                switch (Consulta.TipoConsulta)
                {
                    case TypeQueryEnum.INSERT:
                        Resultado = ResultadoTipoInsert;
                        break;
                    case TypeQueryEnum.UPDATE:
                        Resultado = ResultadoTipoUpdate;
                        break;
                    case TypeQueryEnum.DELETE:
                        Resultado = ResultadoTipoDelete;
                        break;
                    case TypeQueryEnum.SELECT:
                        Resultado = ConvertirResultadoQuery<T>();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception Ex)
            {
                Excepcion = Ex;
                Resultado = default(T);
            }

            return (T)Resultado;
        }

        private T ConvertirResultadoQuery<T>()
        {
            bool IsList = false;
            Object Resultado = null;
            Type ArgumentType = null;
            Type Tipo = null;

            var CurrentType = typeof(T);
            if (CurrentType.IsGenericType && CurrentType.GetGenericTypeDefinition() == typeof(List<>))
            {
                var Args = CurrentType.GetGenericArguments();
                ArgumentType = CurrentType.GetGenericArguments()[0];
                var IListRef = typeof(List<>);
                Type[] IListParam = { ArgumentType };

                Resultado = Activator.CreateInstance(IListRef.MakeGenericType(IListParam));
                IsList = true;
                Tipo = ArgumentType;
            }
            else
            {
                IsList = false;
                Tipo = typeof(T);
            }

            string[] Propiedades = Tipo.GetProperties().Select(x => x.Name).ToArray();
            PropertyInfo[] PropInfo = Tipo.GetProperties();

            foreach (DataRow Row in ResultadoTipoQuery.Rows)
            {
                Object obj = null;
                if (IsList)
                {
                    obj = Activator.CreateInstance(ArgumentType);
                }
                else
                {
                    obj = Activator.CreateInstance(typeof(T));
                }

                for (int i = 0; i < Propiedades.Length; i++)
                {
                    if (ResultadoTipoQuery.Columns.Contains(Propiedades[i]))
                    {
                        object DataCell = Row[Propiedades[i]];
                        Type TypeCell = PropInfo[i].PropertyType;

                        if (!Row.IsNull(Propiedades[i]) || !(System.DBNull.Value == DataCell))
                        {
                            if (TypeCell.IsGenericType && TypeCell.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                            {
                                TypeCell = Nullable.GetUnderlyingType(TypeCell);
                            }

                            PropInfo[i].SetValue(obj, Convert.ChangeType(DataCell, TypeCell));
                        }
                    }
                }

                if (IsList)
                {
                    Resultado.GetType().GetMethod("Add").Invoke(Resultado, new[] { obj });
                }
                else
                {
                    return (T)obj;
                }
            }

            return (T)Resultado;
        }
    }
}
