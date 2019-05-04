using System;
using System.Collections.Generic;

namespace HotelBahia.DataAccess.Models
{
    public partial class TipoHabitacion
    {
        public TipoHabitacion()
        {
            Habitacion = new HashSet<Habitacion>();
        }

        public int TipoHabitacionId { get; set; }
        public string Nombre { get; set; }

        public ICollection<Habitacion> Habitacion { get; set; }
    }
}
