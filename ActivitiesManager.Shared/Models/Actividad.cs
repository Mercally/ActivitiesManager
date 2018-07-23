using System;
using System.Collections.Generic;
using System.Text;
using ActivitiesManager.Shared.Notations.Data;

namespace ActivitiesManager.Shared.Models
{
    [Table("actmgr.Actividades")]
    public class Actividad
    {
        [Key]
        [Column]
        public int Id { get; set; }
        [Column]
        public string Nombre { get; set; }
        [Column]
        public int ProyectoId { get; set; }

        [ForeignKey("actmgr.Proyectos", "Id")]
        public Proyecto Proyecto { get; set; }
    }
}
