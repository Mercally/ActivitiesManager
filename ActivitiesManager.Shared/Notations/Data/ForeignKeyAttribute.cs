using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Shared.Notations.Data
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ForeignKeyAttribute : Attribute
    {
        public ForeignKeyAttribute(string TableName, string ColumnForeignKeyNam)
        {
            this.TableName = TableName;
            this.ColumnForeignKeyNam = ColumnForeignKeyNam;
        }

        public ForeignKeyAttribute() { }

        public string TableName { get; set; }
        public string ColumnForeignKeyNam { get; set; }
    }
}
