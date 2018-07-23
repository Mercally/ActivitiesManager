using ActivitiesManager.Data.Models;
using ActivitiesManager.Data.Models.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Data.Interfaces
{
    public interface ICustomContext
    {

        void CommitOrRollback();

        void Commit();

        void Save(string SavePointName);

        void Rollback();

        QueryResult Ejecutar(QueryBuilder Consulta);

        void Excepciones(Exception Excepcion);
    }
}
