using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.Presentacion.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class SupervisionController : Controller
    {
        private readonly IHabitacionRepository _habitacionRepository;
        public SupervisionController(IHabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }

        [Authorize(Roles = "Supervisor")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Supervisor")]
        [Route("[Controller]/{idHabitacion}")]
        public IActionResult Supervisar([FromRoute]int idHabitacion)
        {
            var habitacion = _habitacionRepository.ObtenerConActividades(idHabitacion, (int)ActividadTipo.Supervision);
            if (habitacion != null)
            {
                NotFound();
            }
            return View(habitacion);
        }
    }
}