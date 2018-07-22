using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace ActivitiesManager.Data.Interfaces
{
    public interface IBaseDeDatos
    {
        SqlConnection Connect(string ConnectionName);
    }
}
