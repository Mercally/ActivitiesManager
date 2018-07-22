using ActivitiesManager.Data.Models;
using ActivitiesManager.Data.Models.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ActivitiesManager.Data.Connections
{
    internal class Comandos
    {
        /// <summary>
        /// Constructor público por defecto
        /// </summary>
        /// <param name="Transaccion"></param>
        public Comandos(SqlTransaction Transaccion)
        {
            this.Transaccion = Transaccion;
        }

        private Comandos() { }

        private SqlTransaction Transaccion { get; }

        /// <summary>
        /// Retorna una tabla de acuerdo a la consulta proporcionada
        /// </summary>
        /// <param name="Consulta">Consulta a ejecutar</param>
        /// <returns></returns>
        public DataTable ExecuteQuery(QueryBuilder Consulta)
        {
            if (Transaccion.Connection.State != ConnectionState.Open)
            {
                Transaccion.Connection.Open();
            }

            SqlCommand cmd = new SqlCommand(Consulta.ConsultaCruda, Transaccion.Connection, Transaccion);
            cmd.CommandTimeout = Consulta.TimeOut;
            cmd.Parameters.AddRange(Consulta.Parametros.ToArray());

            var SqlDataReader = cmd.ExecuteReader();
            var DT = new DataTable();
            DT.Load(SqlDataReader);

            return DT;
        }

        /// <summary>
        /// Ejecuta comandos delete y update
        /// </summary>
        /// <param name="Consulta">Consulta a ejecutar</param>
        /// <returns></returns>
        public bool ExecuteNonQuery(QueryBuilder Consulta)
        {
            if (Transaccion.Connection.State != ConnectionState.Open)
            {
                Transaccion.Connection.Open();
            }

            SqlCommand cmd = new SqlCommand(Consulta.ConsultaCruda, Transaccion.Connection, Transaccion);
            cmd.CommandTimeout = Consulta.TimeOut;
            cmd.Parameters.AddRange(Consulta.Parametros.ToArray());

            int Changes = cmd.ExecuteNonQuery();

            return Changes > 0 || true; // Si no hay cambios
        }

        /// <summary>
        /// Ejecuta un comando que devuelve un solo valor
        /// </summary>
        /// <param name="Consulta">Consulta a ejecutar</param>
        /// <returns></returns>
        public int ExecuteScalarInsert(QueryBuilder Consulta)
        {
            if (Transaccion.Connection.State != ConnectionState.Open)
            {
                Transaccion.Connection.Open();
            }

            SqlCommand cmd = new SqlCommand(Consulta.ConsultaCruda, Transaccion.Connection, Transaccion);
            cmd.CommandTimeout = Consulta.TimeOut;
            cmd.Parameters.AddRange(Consulta.Parametros.ToArray());

            int Result = 0;
            try
            {
                Result = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                Result = 0;
            }

            return Result;
        }

        /// <summary>
        /// Ejecuta un comando que devuelve un solo valor
        /// </summary>
        /// <param name="Consulta">Consulta a ejecutar</param>
        /// <returns></returns>
        public object ExecuteScalar(QueryBuilder Consulta)
        {
            object Result = null;

            if (Transaccion.Connection.State != ConnectionState.Open)
            {
                Transaccion.Connection.Open();
            }

            SqlCommand cmd = new SqlCommand(Consulta.ConsultaCruda, Transaccion.Connection, Transaccion);
            cmd.CommandTimeout = Consulta.TimeOut;
            cmd.Parameters.AddRange(Consulta.Parametros.ToArray());

            Result = cmd.ExecuteScalar();

            return Result;
        }

        /// <summary>
        /// Ejecuta la consulta de acuerdo a la especificacion del tipo de consulta
        /// </summary>
        /// <param name="Consulta">Consulta a ejecutar</param>
        /// <returns>objeto segun tipo de transaccion</returns>
        public QueryResult Ejecutar(QueryBuilder Consulta)
        {
            QueryResult Resultado = new QueryResult();

            switch (Consulta.TipoConsulta)
            {
                case TypeQueryEnum.INSERT:
                    Resultado.ResultadoTipoInsert = ExecuteScalarInsert(Consulta);
                    break;
                case TypeQueryEnum.UPDATE:
                    Resultado.ResultadoTipoUpdate = ExecuteNonQuery(Consulta);
                    break;
                case TypeQueryEnum.DELETE:
                    Resultado.ResultadoTipoDelete = ExecuteNonQuery(Consulta);
                    break;
                case TypeQueryEnum.SELECT:
                    Resultado.ResultadoTipoQuery = ExecuteQuery(Consulta);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("No existe la opción especificada");
            }

            return Resultado;
        }
    }
}
