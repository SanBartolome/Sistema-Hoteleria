using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Servicios;
using HotelBahia.Presentacion.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly IHabitacionRepository _habitacionRepository;
        public CheckOutController(IHabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(int nroHabitacion)
        {
            //Message msj = new Message();
            //if (_habitacionService.CheckOut(_habitacionService.BuscarPorNro(nroHabitacion)))
            //{
            //    msj.Tipo = MessageType.success;
            //    msj.Contenido = "Se realizó el CheckOut de la Habitacion " + nroHabitacion;
            //}
            //else
            //{
            //    msj.Tipo = MessageType.danger;
            //    msj.Contenido = string.Format("La Habitacion {0} no puede realizar CheckOut", nroHabitacion);
            //}
            //ViewData["Mensaje"] = msj;
            return View("Index");
        }
    }
}