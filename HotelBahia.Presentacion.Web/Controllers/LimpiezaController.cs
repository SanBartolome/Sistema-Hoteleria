using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.BussinesLogic.Servicios;
using HotelBahia.Presentacion.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class LimpiezaController : Controller
    {
        private IHabitacionRepository _habitacionRepository;
        public LimpiezaController(IHabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }
        public IActionResult ListHabitacion()
        {
            return View();
        }

        [HttpGet]
        [Route("[Controller]/{idHabitacion}")]
        public IActionResult RealizarLimpieza(int idHabitacion)
        {
            RealizarLimpiezaViewModel model = new RealizarLimpiezaViewModel();
            var habitacion = _habitacionRepository.ObtenerConActividades(idHabitacion, (int)ActividadTipo.Limpieza);
            if (habitacion != null)
            {
                model.Habitacion = habitacion;
            }
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}