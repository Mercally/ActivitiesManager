using ActivitiesManager.Business.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Business
{
    public interface IServiceBusiness
    {
        IServiceComponent ProyectoBusiness { get; }
    }
}
