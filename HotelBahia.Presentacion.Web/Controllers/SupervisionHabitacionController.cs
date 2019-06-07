using HotelBahia.Presentacion.Web.Models;
using HotelBahia.Presentacion.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class SupervisionHabitacionController : Controller
    {
        public IActionResult SupervisarHabitacion(int idHabitacion)
        {
            SupervisarHabitacionViewModel supervision = new SupervisarHabitacionViewModel();
            ReporteHabitacionModel rephab = supervision.ObtenerReporte(idHabitacion);
            return View(rephab);
        }

        public IActionResult ListarHabitaciones(int idHabitacion)
        {
            MantenimientoHabitacionViewModel vm = new MantenimientoHabitacionViewModel();
            List<HabitacionesModel> habitacionmodel = vm.ListarHabitacion();
            return View(habitacionmodel);
        }
    }
}