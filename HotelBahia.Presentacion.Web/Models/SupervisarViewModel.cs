using HotelBahia.Presentacion.Web.Models.Estados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models
{
    public class SupervisarViewModel
    {
        public int IdHabitacion { get; set; }
        public int NroHabitacion { get; set; }
        public HabitacionEstado EstadoHabitacion { get; set; }
        public string Usuario { get; set; }
        public List<ActividadSupervisar> Actividades { get; set; }
    }

    public class ActividadSupervisar
    {
        public int IdActividad { get; set; }
        public string Descripcion { get; set; }
        public ActividadEstado Estado { get; set; }
    }
}