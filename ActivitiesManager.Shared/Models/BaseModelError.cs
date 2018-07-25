using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Shared.Models
{
    public class BaseModelError
    {
        public Exception Exception { get; set; }

        public bool HasException
        {
            get
            {
                return (Exception != null);
            }
        }

        public bool HasInnerException
        {
            get
            {
                if (this.HasException)
                {
                    return (this.Exception.InnerException != null);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
