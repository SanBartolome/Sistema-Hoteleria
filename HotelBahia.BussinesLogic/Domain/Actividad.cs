using System;
using System.Collections.Generic;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class Actividad
    {
        public Actividad()
        {
            HabitacionActividad = new HashSet<HabitacionActividad>();
        }

        public int ActividadId { get; set; }
        public int TipoActividadId { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }

        public TipoActividad TipoActividad { get; set; }
        public ICollection<HabitacionActividad> HabitacionActividad { get; set; }
    }
}
