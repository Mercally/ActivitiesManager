﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace ActivitiesManager.Data.Models.Query
{
    public class QueryBuilder
    {
        public QueryBuilder(int TimeOut = 1)
        {
            this.ConsultaCruda = string.Empty;
            this.Parametros = new List<SqlParameter>();
            this.TieneError = false;
            this.TimeOut = TimeOut;
            this.Includes = new string[] { };
        }

        public QueryBuilder(string ConsultaCruda, TypeQueryEnum TypeQueryEnum, List<SqlParameter> Params = null, int TimeOut = 1)
        {
            this.TipoConsulta = TypeQueryEnum;
            this.ConsultaCruda = ConsultaCruda;
            this.Parametros = Params ?? new List<SqlParameter>();
            this.TieneError = false;
            this.TimeOut = TimeOut;
            this.Includes = new string[] { };
        }

        /// <summary>
        /// Consulta en forma de cadena T-SQL
        /// </summary>
        public string ConsultaCruda { get; set; }

        /// <summary>
        /// Lista de parámetros según nombre en ConsultaCruda
        /// </summary>
        public List<SqlParameter> Parametros { get; set; }

        /// <summary>
        /// Tipo de consulta a realizar: Insert, Update, Delete, Query
        /// </summary>
        public TypeQueryEnum TipoConsulta { get; set; }

        /// <summary>
        /// Tiempo de espera en segundos para cada comando de forma individual
        /// </summary>
        public int TimeOut { get; set; }

        /// <summary>
        /// Determina si la consulta tiene errores
        /// </summary>
        public bool TieneError { get; internal set; }

        private string[] Includes { get; set; }

        public Type PrincipalType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Includes"></param>
        /// <returns></returns>
        public QueryBuilder Include(string Includes)
        {
            if (string.IsNullOrEmpty(Includes))
            {
                throw new ArgumentNullException("Includes es un parámetro obligatorio. Contiene propiedades a inicializar sepadatas por punto.");
            }

            this.Includes = Includes.Split(".");



            return this;
        }

        /// <summary>
        /// Crea SELECT
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static QueryBuilder CreateQuery(Type type)
        {
            QueryBuilder Query = new QueryBuilder();

            Query = CreateSELECT(null, type);
            Query.TipoConsulta = TypeQueryEnum.SELECT;
            Query.PrincipalType = type;

            return Query;
        }

        /// <summary>
        /// Crea SELECT con condicionante por Id
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id">Valor de la columna a condicionar</param>
        /// <param name="columnKey">Nombre de la columna</param>
        /// <returns></returns>
        public static QueryBuilder CreateQueryById(Type type, int id, string columnKey = null)
        {
            QueryBuilder Query = new QueryBuilder();

            Query = CreateSELECT(null, type);
            Query.ConsultaCruda = 
                Query.ConsultaCruda.Replace(";", $" WHERE {columnKey ?? "Id"} = {id};");
            Query.TipoConsulta = TypeQueryEnum.SELECT;
            Query.PrincipalType = type;

            return Query;
        }

        public static QueryBuilder CreateQuery(Object Obj, TypeQueryEnum TipoConsulta, bool IsDeleteLogical = true, int TimeOut = 1)
        {
            QueryBuilder Query = new QueryBuilder();

            switch (TipoConsulta)
            {
                case TypeQueryEnum.INSERT:
                    Query = CreateINSERT(Obj);
                    break;
                case TypeQueryEnum.UPDATE:
                    Query = CreateUPDATE(Obj);
                    break;
                case TypeQueryEnum.DELETE:
                    Query = CreateDELETE(Obj, IsDeleteLogical);
                    break;
                case TypeQueryEnum.SELECT:
                    Query = CreateSELECT(Obj);
                    break;
                default:
                    break;
            }

            Query.TipoConsulta = TipoConsulta;
            return Query;
        }

        /// <summary>
        /// Crea una consulta tipo SELECT basica a partir de una clase
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        private static QueryBuilder CreateSELECT(Object Obj, Type type = null)
        {
            Type Tipo = null;
            if (type == null)
            {
                if (Obj == null)
                {
                    throw new ArgumentNullException("Parámetro obj no debe ser nulo. Parámetro type es opcional, ambos parámetros deben ser del mismo tipo.");
                }
                else
                {
                    Tipo = Obj.GetType();
                }
            }
            else
            {
                Tipo = type;
            }
            
            var TableName = Tipo.GetCustomAttributesData()[0].ConstructorArguments[0].Value;
            PropertyInfo[] PropInfo = Tipo.GetProperties();

            string SqlQuery = $"SELECT ";

            foreach (var Prop in PropInfo)
            {
                var Attrs = Prop.GetCustomAttributes().Select(x => x.GetType().Name).ToArray();

                if (Attrs.Contains("ColumnAttribute"))
                    SqlQuery += Prop.Name + ", ";
            }

            SqlQuery = SqlQuery.Substring(0, (SqlQuery.Length - 2));
            SqlQuery += $" FROM {TableName};";

            return new QueryBuilder(SqlQuery, TypeQueryEnum.SELECT);
        }

        /// <summary>
        /// Crea una consulta tipo delete a partir de una clases
        /// </summary>
        /// <param name="Obj"></param>
        /// <returns></returns>
        private static QueryBuilder CreateINSERT(Object Obj)
        {
            Type Tipo = Obj.GetType();
            var TableName = Tipo.GetCustomAttributesData()[0].ConstructorArguments[0].Value;

            string SqlQuery = string.Empty;
            string Insert = $"INSERT INTO {TableName}(";
            string Values = ")VALUES(";

            List<SqlParameter> ListParameters = new List<SqlParameter>();

            PropertyInfo[] PropInfo = Tipo.GetProperties();

            foreach (var Prop in PropInfo)
            {
                var Attrs = Prop.GetCustomAttributes().Select(x => x.GetType().Name).ToArray();

                if (Attrs.Contains("KeyAttribute"))
                {
                    continue;
                }
                else if (Attrs.Contains("ColumnAttribute"))
                {
                    string Column = Prop.Name;
                    object Value = Prop.GetValue(Obj);

                    if (Value != null)
                    {
                        Insert += Column + ", ";

                        string Param = "@" + Column;
                        Values += Param + ", ";

                        ListParameters.Add(new SqlParameter(Param, Value));
                    }
                }
            }

            Insert = Insert.Substring(0, (Insert.Length - 2));
            Values = Values.Substring(0, (Values.Length - 2));
            SqlQuery = Insert + Values + "); SELECT SCOPE_IDENTITY();";

            return new QueryBuilder(SqlQuery, TypeQueryEnum.INSERT, ListParameters);
        }

        /// <summary>
        /// Crea una consulta tipo Update a partir de una clase
        /// </summary>
        /// <param name="Obj">Objeto</param>
        /// <returns></returns>
        private static QueryBuilder CreateUPDATE(Object Obj)
        {
            Type Tipo = Obj.GetType();

            var TableName = Tipo.GetCustomAttributesData()[0].ConstructorArguments[0].Value;

            string SqlQuery = $"UPDATE {TableName} SET ";
            string Where = string.Empty;

            List<SqlParameter> ListParameters = new List<SqlParameter>();


            PropertyInfo[] PropInfo = Tipo.GetProperties();

            foreach (var Prop in PropInfo)
            {
                var Attrs = Prop.GetCustomAttributes().Select(x => x.GetType().Name).ToArray();

                string Column = Prop.Name;
                string Param = $"@{Column}";

                object Value = Prop.GetValue(Obj);

                if (Attrs.Contains("KeyAttribute"))
                {
                    Where = $" WHERE {Column}={Param};";
                    ListParameters.Add(new SqlParameter(Param, Value));
                    continue;
                }
                else if (Attrs.Contains("ColumnAttribute"))
                {
                    SqlQuery += $"{Column}={Param}, ";
                    ListParameters.Add(new SqlParameter(Param, Value));
                }
            }

            SqlQuery = SqlQuery.Substring(0, (SqlQuery.Length - 2));
            SqlQuery += Where;

            return new QueryBuilder(SqlQuery, TypeQueryEnum.UPDATE, ListParameters);
        }

        /// <summary>
        /// Crea una consulta tipo Delete a partir de una clase
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="IsLogical"></param>
        /// <returns></returns>
        private static QueryBuilder CreateDELETE(Object Obj, bool IsLogical)
        {
            Type Tipo = Obj.GetType();
            var TableName = Tipo.GetCustomAttributesData()[0].ConstructorArguments[0].Value;
            string SqlQuery = string.Empty;
            TypeQueryEnum TypeQuery = TypeQueryEnum.SELECT;
            List<SqlParameter> ListParameters = new List<SqlParameter>();
            if (IsLogical)
            {
                SqlQuery = $"UPDATE {TableName} SET EsActivo = 0;";
                TypeQuery = TypeQueryEnum.UPDATE;
            }
            else
            {
                SqlQuery = $"DELETE {TableName} WHERE Id = @Id;";
                ListParameters.Add(new SqlParameter("@Id", 1));
                TypeQuery = TypeQueryEnum.DELETE;
            }

            return new QueryBuilder(SqlQuery, TypeQuery, ListParameters);
        }
    }
}
