using System;
using System.Collections.Generic;

namespace HotelBahia.DataAccess.Models
{
    public partial class EstadoHabitacion
    {
        public EstadoHabitacion()
        {
            Habitacion = new HashSet<Habitacion>();
        }

        public int EstadoHabitacionId { get; set; }
        public string EstadoNombre { get; set; }

        public ICollection<Habitacion> Habitacion { get; set; }
    }
}
