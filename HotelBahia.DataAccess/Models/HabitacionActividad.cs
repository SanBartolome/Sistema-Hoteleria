using System;
using System.Collections.Generic;

namespace HotelBahia.DataAccess.Models
{
    public partial class HabitacionActividad
    {
        public int HabitacionActividadId { get; set; }
        public int? HabitacionId { get; set; }
        public int? ActividadId { get; set; }
        public int? Estado { get; set; }

        public Actividad Actividad { get; set; }
        public Habitacion Habitacion { get; set; }
    }
}
