using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.BussinesLogic.Servicios;
using HotelBahia.DataAccess.Models;
using HotelBahia.Presentacion.Web.Helpers;
using HotelBahia.Presentacion.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

        [HttpGet]
        [Route("[Controller]/[Action]/{idHabitacion}")]
        public IActionResult RealizarLimpieza(int idHabitacion)
        {
            RealizarLimpiezaViewModel model = new RealizarLimpiezaViewModel();
            if (TempData["Mensaje"] != null)
            {
                ViewData["Mensaje"] = JsonConvert.DeserializeObject<Message>((string)TempData["Mensaje"]);
            }
            var habitacion = _habitacionService.ObtenerConActividades(idHabitacion, ActividadTipo.Limpieza);
            if (habitacion != null)
            {
                model.Habitacion = habitacion;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult RealizarLimpiezaPost(int idHabitacion)
        {
            Message msj = new Message();
            if (_habitacionService.RealizarLimpieza(idHabitacion))
            {
                msj.Tipo = MessageType.success;
                msj.Contenido = "Se registro correctamente la limpieza";
            }
            else
            {
                msj.Tipo = MessageType.danger;
                msj.Contenido = "Ocurrio un error al marcar la limpieza.\n Intente Nuevamente";
            }
            TempData["Mensaje"] = JsonConvert.SerializeObject(msj);
            return RedirectToAction("RealizarLimpieza", new { idHabitacion = idHabitacion });
            //return View("RealizarLimpieza", model);
        }
    }
}