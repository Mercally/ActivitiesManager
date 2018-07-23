using ActivitiesManager.Shared.Notations.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ActivitiesManager.Shared.Models
{
    [Table("actmgr.Proyectos")]
    public class Proyecto
    {
        public Proyecto()
        {
            Actividades = new List<Actividad>();
        }

        [Key]
        [Column]
        public int Id { get; set; }
        [Column]
        public string Nombre { get; set; }

        [ForeignKey("actmgr.Actividades", "ProyectoId")]
        public List<Actividad> Actividades { get; set; }
    }
}
