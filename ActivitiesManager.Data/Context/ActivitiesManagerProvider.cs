using ActivitiesManager.Data.Connections;
using ActivitiesManager.Data.Interfaces;
using ActivitiesManager.Data.Models;
using ActivitiesManager.Data.Models.Query;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ActivitiesManager.Data.Context
{
    /// <summary>
    /// Almacena todas las clases de modelo de la base de datos
    /// </summary>
    public class ActivitiesManagerProvider : IDisposable, ICustomContext, IActivitiesManagerProvider
    {
        public IBaseDeDatos BaseDeDatos { get; set; }

        /// <summary>
        /// Constructor por defecto
        /// </summary>
        public ActivitiesManagerProvider(IBaseDeDatos baseDeDatos)
        {
            BaseDeDatos = baseDeDatos;
            this.ListaExcepciones = new List<Exception>();

            try
            {
                Byte Pasos = 0;
                // Inicializa una conexion:
                if (this.Conexion == null)
                {
                    this.Conexion = BaseDeDatos.Connect("ActivitiesManagerDataBase");
                    Pasos += 1;
                }

                // Iniciliza una transaccion:
                if (this.Transaccion == null && this.Conexion != null)
                {
                    this.Transaccion = Conexion.BeginTransaction();
                    Pasos += 2;
                }

                if (Pasos == 3)
                {
                    Comandos = new Comandos(Transaccion);
                }
            }
            catch (Exception ex)
            {
                ListaExcepciones.Add(ex);
                throw new Exception("No se logró establecer la conexión con la base de datos.", ex);
            }
        }

        private Comandos Comandos { get; set; }
        public SqlConnection Conexion { get; protected set; }
        public SqlTransaction Transaccion { get; protected set; }
        public List<Exception> ListaExcepciones { get; protected set; }
        public int TimeOut { get; protected set; }

        public void Excepciones(Exception Ex)
        {
            if (ListaExcepciones != null)
            {
                if (Ex != null)
                {
                    ListaExcepciones = new List<Exception>() { Ex };
                }
                else
                {
                    ListaExcepciones = new List<Exception>();
                }
            }
            else
            {
                if (Ex != null)
                {
                    ListaExcepciones.Add(Ex);
                }
            }
        }

        /// <summary>
        /// Guarda el estado de la transacción a un puto especifico, si se llama a rollback
        /// se realiza hasta este punto.
        /// </summary>
        public void Save(string SavePointName)
        {
            if (Transaccion != null)
                Transaccion.Save(SavePointName);
        }

        /// <summary>
        /// Realiza rollback a un punto guardado de la transacción
        /// </summary>
        public void Rollback()
        {
            if (Transaccion != null)
                Transaccion.Rollback();
        }

        /// <summary>
        /// Guarda los cambios dentro de la base de datos
        /// </summary>
        public void Commit()
        {
            if (Transaccion != null)
                Transaccion.Commit();
        }

        /// <summary>
        /// Guarda la transacción si ha sido exitosa. Si ha fallado, se realiza Rollback.
        /// </summary>
        public void CommitOrRollback()
        {
            if (ListaExcepciones.Count > 0)
            {
                Rollback();

                // Registrar excepciones
            }
            else
            {
                Commit();
            }
        }

        /// <summary>
        /// Libera recursos utilizados en la transacción
        /// </summary>
        public void Dispose()
        {
            CommitOrRollback();

            Transaccion.Dispose();
            Conexion.Dispose();
        }

        /// <summary>
        /// Ejecuta una consulta
        /// </summary>
        /// <param name="Consulta">Consulta a ejecutar</param>
        /// <returns></returns>
        public QueryResult Ejecutar(QueryBuilder Consulta)
        {
            return Comandos.Ejecutar(Consulta);
        }

        /*
         * Accesores a modelos
         */

        public Actmgr_Proyectos Actmgr_Proyectos
        {
            get
            {
                return new Actmgr_Proyectos(this);
            }
        }
    }
}
