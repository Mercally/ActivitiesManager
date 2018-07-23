using ActivitiesManager.Business.Common;

namespace ActivitiesManager.Business
{
    public class ServiceBusiness : IServiceBusiness
    {
        public ServiceBusiness()
        {

        }

        public ProyectoBusiness ProyectoBusiness { get { return new ProyectoBusiness(); } }

        public ActividadBusiness ActividadBusiness { get { return new ActividadBusiness(); } }
    }
}
