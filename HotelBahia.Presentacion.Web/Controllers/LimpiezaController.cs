using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.BussinesLogic.Servicios;
using HotelBahia.Presentacion.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class LimpiezaController : Controller
    {
        private HabitacionService _habitacionService; 
        public LimpiezaController(HabitacionService habitacionService)
        {
            _habitacionService = habitacionService;
        }
        public IActionResult ListHabitacion()
        {
            return View();
        }
        [Route("[Controller]/[Action]/{idHabitacion}")]
        public IActionResult RealizarLimpieza(int idHabitacion)
        {
            RealizarLimpiezaViewModel model = new RealizarLimpiezaViewModel();
            var habitacion = _habitacionService.ObtenerConActividades(idHabitacion, ActividadTipo.Limpieza);
            if (habitacion != null)
            {
                model.habitacion = habitacion;
            }
            return View(model);
        }
    }
}