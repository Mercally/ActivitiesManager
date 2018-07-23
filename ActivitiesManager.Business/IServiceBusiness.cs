using ActivitiesManager.Business.Common;

namespace ActivitiesManager.Business
{
    public interface IServiceBusiness
    {
        ProyectoBusiness ProyectoBusiness { get; }
        ActividadBusiness ActividadBusiness { get; }
    }
}
