using ActivitiesManager.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Data.Interfaces
{
    public interface IActivitiesManagerProvider
    {
        Actmgr_Proyectos Actmgr_Proyectos { get; }
    }
}
