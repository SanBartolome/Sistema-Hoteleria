using HotelBahia.BussinesLogic.Contracts.Repositories;
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