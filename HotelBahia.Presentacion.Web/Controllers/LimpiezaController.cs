using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.BussinesLogic.Servicios;
using HotelBahia.DataAccess.Context;
using HotelBahia.Presentacion.Web.Controllers.Base;
using HotelBahia.Presentacion.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class LimpiezaController : BaseController
    {
        private IHabitacionRepository _habitacionRepository;
        private SignInManager<UserLogin> _signInManager;
        private HoteleriaContext _context;
        private UserManager<UserLogin> _userManager;

        public LimpiezaController(IHabitacionRepository habitacionRepository, SignInManager<UserLogin> signInManager, HoteleriaContext context,
        UserManager<UserLogin> userManager)
        {
            _habitacionRepository = habitacionRepository;
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult ListHabitacion()
        {
            return View();
        }



        [HttpGet]
        [Authorize(Roles = "Limpieza")]
        [Route("[Controller]/{idHabitacion}")]
        public IActionResult RealizarLimpieza(int idHabitacion)
        {
            RealizarLimpiezaViewModel model = new RealizarLimpiezaViewModel();
            var empleado = _context.Empleado.FirstOrDefault( e => e.UsuarioNombre == _userManager.GetUserName(User) );
            var asignacion = _context.AsignacionHabitacion.FirstOrDefault(asig => asig.HabitacionId == idHabitacion && asig.EmpleadoId == empleado.EmpleadoId);
            if ( asignacion == null )
            {
                alert("error", "Usted no se encuentra autorizado para acceder", "No autorizado");
                return RedirectToAction("login", "account");
            }
            var habitacion = _habitacionRepository.ObtenerConActividades(idHabitacion, (int)ActividadTipo.Limpieza);
            if (habitacion != null)
            {
                model.Habitacion = habitacion;
            }
            return View(model);
        }

        [Authorize(Roles = "Limpieza")]
        public IActionResult Index()
        {
            return View();
        }


    }
}