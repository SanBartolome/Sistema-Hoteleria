using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelBahia.Presentacion.Web.Controllers.Base
{
    public abstract class BaseController : Controller
    {
        
        protected void alert(string type, string message, string title = "")
        {
            Response.Cookies.Append("notificacion", $"toast.{type}('{message}','{title}')", new CookieOptions { IsEssential = true });
        }

    }
}
