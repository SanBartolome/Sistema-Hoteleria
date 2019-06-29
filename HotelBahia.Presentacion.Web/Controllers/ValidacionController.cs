using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HotelBahia.BussinesLogic.Domain.Enums;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public abstract class ValidacionController : Controller
    {
        public void Alert(string message, ValidacionType validacionType)
        {
            var msg = "swal('" + validacionType.ToString().ToUpper() + "', '" + message + "','" + validacionType + "')" + "";
            TempData["validacion"] = msg;
        }
    }
}