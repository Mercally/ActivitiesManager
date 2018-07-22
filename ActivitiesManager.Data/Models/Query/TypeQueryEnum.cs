using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Data.Models.Query
{    
    /// <summary>
    /// Tipo de consulta a ejecutar
    /// </summary>
    public enum TypeQueryEnum
    {
        /// <summary>
        /// Insert seguido de un select del ID recien ingresado, retorna dicho ID
        /// </summary>
        INSERT = 0,

        /// <summary>
        /// Update del registro
        /// </summary>
        UPDATE = 1,

        /// <summary>
        /// Delete del registro
        /// </summary>
        DELETE = 2,

        /// <summary>
        /// Consulta
        /// </summary>
        SELECT = 3
    }
}
