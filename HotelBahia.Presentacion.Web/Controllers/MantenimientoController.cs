using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.DataAccess.Context;
using HotelBahia.Presentacion.Web.Controllers.Base;
using HotelBahia.Presentacion.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class MantenimientoController : BaseController
    {
        private IHabitacionRepository _habitacionRepository;
        private SignInManager<UserLogin> _signInManager;
        private HoteleriaContext _context;
        private UserManager<UserLogin> _userManager;
        public MantenimientoController(IHabitacionRepository habitacionRepository, SignInManager<UserLogin> signInManager, HoteleriaContext context, UserManager<UserLogin> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
            _habitacionRepository = habitacionRepository;
        }
        public IActionResult ListHabitacion()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Mantenimiento")]
        [Route("[Controller]/{idHabitacion}")]
        public IActionResult RealizarMantenimiento(int idHabitacion)
        {
            RealizarMantenimientoViewModel model = new RealizarMantenimientoViewModel();
            var empleado = _context.Empleado.FirstOrDefault(e => e.UsuarioNombre == _userManager.GetUserName(User));
            var asignacion = _context.AsignacionHabitacion.FirstOrDefault(asig => asig.HabitacionId == idHabitacion && asig.EmpleadoId == empleado.EmpleadoId);
            if (asignacion == null)
            {
                alert("error", "Usted no se encuentra autorizado para acceder", "No autorizado");
                return RedirectToAction("login", "account");
            }
            var habitacion = _habitacionRepository.ObtenerConIncidencias(idHabitacion);
            var incidencia = _context.Incidencia.Where(i => i.Habitacion == habitacion.Numero).First();
            if (habitacion != null && incidencia != null)
            {
                model.Habitacion = habitacion;
                model.Incidencia = incidencia;
            }
            return View(model);
        }

        [Authorize(Roles = "Mantenimiento")]
        public IActionResult Index()
        {
            return View();
        }
    }
}