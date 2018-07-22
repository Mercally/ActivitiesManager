using System;
using System.Collections.Generic;
using System.Text;
using ActivitiesManager.Business.Common;

namespace ActivitiesManager.Business
{
    public class ServiceBusiness : IServiceBusiness
    {
        public ServiceBusiness(
            IServiceComponent serviceComponent
            )
        {
            ProyectoBusiness = serviceComponent;
        }

        public IServiceComponent ProyectoBusiness { get; }
    }
}
