using ActivitiesManager.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.IO;

namespace ActivitiesManager.Data.Connections
{
    /// <summary>
    /// Establece conexión con las bases de datos
    /// </summary>
    internal class BasesDeDatos
    {
        public static IConfiguration Configuration { get; set; }

        private static string GetConnectionString(string ConnectionName)
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            return Configuration.GetConnectionString(ConnectionName);
        }

        /// <summary>
        /// Establece, abre y retorna la conexión con la base de datos.
        /// </summary>
        /// <returns>Conexión</returns>
        public static SqlConnection Connect(string ConnectionName)
        {
            SqlConnection Connection = null;
            string CadenaDeConexionActivitiesManager = GetConnectionString(ConnectionName);
            Connection = new SqlConnection(CadenaDeConexionActivitiesManager);

            if (Connection.State != System.Data.ConnectionState.Open)
            {
                Connection.Open();
            }

            return Connection;
        }
    }
}
