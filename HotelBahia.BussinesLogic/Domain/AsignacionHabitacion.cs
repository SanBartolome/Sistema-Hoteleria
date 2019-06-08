using System;
using System.Collections.Generic;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class AsignacionHabitacion
    {
        public int AsignacionHabitacionId { get; set; }
        public int EmpleadoId { get; set; }
        public int HabitacionId { get; set; }
        public int RolId { get; set; }
        public DateTime? Fecha { get; set; }

        public Empleado Empleado { get; set; }
        public Habitacion Habitacion { get; set; }
    }
}
