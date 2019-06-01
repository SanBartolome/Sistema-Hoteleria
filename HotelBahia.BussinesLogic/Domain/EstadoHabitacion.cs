using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class EstadoHabitacion
    {
        public EstadoHabitacion()
        {
            Habitacion = new HashSet<Habitacion>();
        }
        public int EstadoHabitacionId { get; set; }
        public string EstadoNombre { get; set; }
        [JsonIgnore]
        public ICollection<Habitacion> Habitacion { get; set; }
    }
}
