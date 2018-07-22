using ActivitiesManager.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ActivitiesManager.Data.Connections
{
    /// <summary>
    /// Establece conexión con las bases de datos
    /// </summary>
    public class BasesDeDatos : IBaseDeDatos
    {
        /// <summary>
        /// Constructor utiliza injección de dependencias para obtener configuración
        /// </summary>
        /// <param name="configuration"></param>
        public BasesDeDatos(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        /// <summary>
        /// Establece, abre y retorna la conexión con la base de datos.
        /// </summary>
        /// <returns>Conexión</returns>
        public SqlConnection Connect(string ConnectionName)
        {
            SqlConnection Connection = null;
            string CadenaDeConexionActivitiesManager = Configuration.GetConnectionString(ConnectionName);
            Connection = new SqlConnection(CadenaDeConexionActivitiesManager);

            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }

            return Connection;
        }
    }
}
