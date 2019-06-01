using System;
using System.Collections.Generic;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class TipoActividad
    {
        public TipoActividad()
        {
            Actividad = new HashSet<Actividad>();
        }

        public int TipoActividadId { get; set; }
        public string Nombre { get; set; }

        public ICollection<Actividad> Actividad { get; set; }
    }
}
