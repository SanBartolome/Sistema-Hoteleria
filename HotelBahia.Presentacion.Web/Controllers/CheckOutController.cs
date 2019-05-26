using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Servicios;
using HotelBahia.DataAccess.Models;
using HotelBahia.Presentacion.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly HabitacionService _habitacionService;
        public CheckOutController(HabitacionService habitacionService)
        {
            _habitacionService = habitacionService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(int nroHabitacion)
        {
            Message msj = new Message();
            if (_habitacionService.CheckOut(_habitacionService.BuscarPorNro(nroHabitacion)))
            {
                msj.Tipo = MessageType.success;
                msj.Contenido = "Se realizó el CheckOut de la Habitacion " + nroHabitacion;
            }
            else
            {
                msj.Tipo = MessageType.danger;
                msj.Contenido = string.Format("La Habitacion {0} no se puede realizar CheckOut", nroHabitacion);
            }
            ViewData["Mensaje"] = msj;
            return View("Index");
        }
    }
}