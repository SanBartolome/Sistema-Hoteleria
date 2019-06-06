using HotelBahia.Presentacion.Web.Models;
using HotelBahia.Presentacion.Web.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class MantenimientoHabitacionesController : Controller
    {
        public IActionResult ListadoHabitaciones()
        {
            MantenimientoHabitacionViewModel vm = new MantenimientoHabitacionViewModel();
            List<HabitacionesModel> habitacionmodel = vm.ListarHabitacion();
            return View(habitacionmodel);
        }

        public IActionResult DesocuparHabitacion(int idHabitacion)
        {
            //MantenimientoHabitacionViewModel vm = new MantenimientoHabitacionViewModel();
            //HabitacionesModel habitacion = new HabitacionesModel();
            //habitacion.idHabitacion = idHabitacion;
            //habitacion.IdEstado = EstadoHabitacion.idOcupado;
            //vm.DesocuparHabitacion(habitacion);
            return View();
        }
    }
}