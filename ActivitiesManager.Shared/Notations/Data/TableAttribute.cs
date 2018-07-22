using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Shared.Notations.Data
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute : Attribute
    {
        public TableAttribute(string TableName)
        {
            this.TableName = TableName;
        }

        private TableAttribute() { }

        public string TableName { get; set; }
    }
}
