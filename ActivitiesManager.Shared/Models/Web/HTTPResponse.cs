using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ActivitiesManager.Shared.Models.Web
{
    public class HttpResponse<T>
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
