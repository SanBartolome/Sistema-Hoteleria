using System.Collections.Generic;

namespace HotelBahia.Presentacion.Web.Models
{
    public class ReporteHabitacionModel
    {

        public int NumeroHab { get; set; }
        public int IdHabitacion { get; set; }
        public List<ActividadModel> Actividades { get; set; }

    }
}
