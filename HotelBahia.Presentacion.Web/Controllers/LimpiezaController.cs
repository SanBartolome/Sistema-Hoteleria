using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class LimpiezaController : Controller
    {
        public IActionResult ListHabitacion()
        {
            return View();
        }
    }
}