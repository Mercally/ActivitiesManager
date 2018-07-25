using ActivitiesManager.Shared.Notations.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DbNotations = ActivitiesManager.Shared.Notations.Data;

namespace ActivitiesManager.Shared.Models
{
    [DbNotations.Table("actmgr.Proyectos")]
    public class Proyecto
    {
        public Proyecto()
        {
            Actividades = new List<Actividad>();
        }

        [DbNotations.Key]
        [DbNotations.Column]
        
        public int Id { get; set; }

        [DbNotations.Column]
        [Display(Name = "Nombres")]
        [MaxLength(10)]
        public string Nombre { get; set; }

        [DbNotations.ForeignKey("actmgr.Actividades", "ProyectoId")]
        public List<Actividad> Actividades { get; set; }
    }
}
