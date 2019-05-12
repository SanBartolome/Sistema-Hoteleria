using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models
{
    public class ReporteHabitacionModel { 
    
        public int NumeroHab { get; set; }
        public int IdHabitacion { get; set; }
        public List<ActividadModel> Actividades{ get; set; }

}
}
