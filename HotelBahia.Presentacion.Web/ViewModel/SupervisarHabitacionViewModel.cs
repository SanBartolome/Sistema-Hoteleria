using HotelBahia.Presentacion.Web.Models;
using System.Collections.Generic;

namespace HotelBahia.Presentacion.Web.ViewModel
{
    public class SupervisarHabitacionViewModel
    {

        public ReporteHabitacionModel ObtenerReporte(int idHabitacion)
        {
            ReporteHabitacionModel reporteHabitacion = new ReporteHabitacionModel();
            reporteHabitacion.Actividades = new List<ActividadModel>();
            return reporteHabitacion;
        }
        public void GuardarReporte(ReporteHabitacionModel reporte, int idUsuario)
        {


        }
    }
}
