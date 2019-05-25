using HotelBahia.Presentacion.Web.Models.Estados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models
{
    public class RealizarLimpiezaViewModel
    {
        private HabitacionEstado estadoHabitacion;
        public int IdHabitacion { get; set; }
        public int NroHabitacion { get; set; }
        public HabitacionEstado EstadoHabitacion {
            get => estadoHabitacion;
            set
            {
                estadoHabitacion = value;
                if (EstadoHabitacion == HabitacionEstado.Desocupado || EstadoHabitacion == HabitacionEstado.LimpiezaIncompleta)
                    EstadoLimpieza = LimpiezaEstado.Asignada;
                else if (EstadoHabitacion == HabitacionEstado.EnLimpieza)
                    EstadoLimpieza = LimpiezaEstado.Iniciada;
                else
                    EstadoLimpieza = LimpiezaEstado.Terminada;
            }
        }

        public string Usuario { get; set; }
        public List<Actividad> Actividades { get; set; }
        public LimpiezaEstado EstadoLimpieza { get; private set; }
    }

    public class Actividad
    {
        public int IdActividad { get; set; }
        public string Descripcion { get; set; }
    }

   
}
