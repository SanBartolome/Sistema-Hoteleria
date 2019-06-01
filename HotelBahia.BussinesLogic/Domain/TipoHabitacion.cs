using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class TipoHabitacion
    {
        public TipoHabitacion()
        {
            Habitacion = new HashSet<Habitacion>();
        }
        public int TipoHabitacionId { get; set; }
        public string Nombre { get; set; }
        [JsonIgnore]
        public ICollection<Habitacion> Habitacion { get; set; }
    }
}
