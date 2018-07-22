using ActivitiesManager.Shared.Notations.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Shared.Models
{
    [Table("actmgr.Proyectos")]
    public class Proyecto
    {
        [Key]
        public int Id { get; set; }
        [Column]
        public string Nombre { get; set; }
    }
}
