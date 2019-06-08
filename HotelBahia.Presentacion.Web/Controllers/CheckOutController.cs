using HotelBahia.BussinesLogic.Contracts.Repositories;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "Administrador")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(int nroHabitacion)
        {
            return View("Index");
        }
    }
}