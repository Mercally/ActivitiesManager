using System.Net;

namespace ActivitiesManager.Shared.Models.Web
{
    public class HttpResponse<T> : BaseModelError
    {
        public T Value { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public bool IsCorrect
        {
            get
            {
                return (StatusCode == HttpStatusCode.OK) && (Value != null);
            }
        }
    }
}
