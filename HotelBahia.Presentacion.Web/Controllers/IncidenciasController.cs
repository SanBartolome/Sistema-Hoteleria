using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.DataAccess.Context;
using HotelBahia.Presentacion.Web.Controllers.Base;
using Microsoft.AspNetCore.Identity;
using HotelBahia.Presentacion.Web.Models;
using HotelBahia.BussinesLogic.Servicios.AppServices;
using HotelBahia.BussinesLogic.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class IncidenciasController : BaseController
    {
        private readonly HoteleriaContext _context;
        private readonly UserManager<UserLogin> _userManager;

        public IncidenciasController(HoteleriaContext context, UserManager<UserLogin> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrador")]
        // GET: Incidencias
        public async Task<IActionResult> Index()
        {
            var hoteleriaContext = _context.Incidencia.Include(i => i.Empleado);
            return View(await hoteleriaContext.ToListAsync());
        }

        [Authorize(Roles = "Administrador")]
        // GET: Incidencias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidencia = await _context.Incidencia
                .Include(i => i.Empleado)
                .FirstOrDefaultAsync(m => m.IncidenciaID == id);
            if (incidencia == null)
            {
                return NotFound();
            }

            return View(incidencia);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Incidencias/Create
        public IActionResult Create([FromQuery(Name = "habitacion")] string habitacionNum)
        {
            if (User.IsInRole("Supervisor"))
            {
                var habitacion = _context.Habitacion.FirstOrDefault(h => h.Numero == int.Parse(habitacionNum));
                if (habitacion == null)
                {
                    return View();
                }
                return View(new Incidencia() { Habitacion = habitacion.Numero });
            }
            ViewData["Habitaciones"] = new SelectList(_context.Habitacion.Where(h => h.EstadoHabitacionId != 7), "Numero", "Numero");
            List<string> prioridad = new List<string>
            {
                "Alta",
                "Media",
                "Baja"
            };
            ViewData["Prioridad"] = new SelectList(prioridad);
            return View();
        }

        // POST: Incidencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IncidenciaID,EmpleadoId,Habitacion,Prioridad,Descripcion,Encargado,Estado,FechaAbierto,FechaCerrado")] Incidencia incidencia)
        {
            if (ModelState.IsValid)
            {
                var empleado = await _context.Empleado.FirstOrDefaultAsync(e => e.UsuarioNombre == _userManager.GetUserName(User));
                incidencia.EmpleadoId = empleado.EmpleadoId;
                incidencia.FechaAbierto = DateTime.Now;
                incidencia.Estado = 0;
                _context.Add(incidencia);
                var habitacion = _context.Habitacion.Where(a => a.Numero == incidencia.Habitacion).FirstOrDefault();
                habitacion.EstadoHabitacionId = 7;
                _context.Update(habitacion);
                await _context.SaveChangesAsync();
                alert("success", "Incidencia creada con éxito", "Operación exitosa");
                if ( User.IsInRole("Supervisor") )
                {
                    return RedirectToAction("index","supervision");
                }
                if ( User.IsInRole("Administrador"))
                {
                    return RedirectToAction("index");
                }
            }
            ViewData["Habitaciones"] = new SelectList(_context.Habitacion.Where(h => h.EstadoHabitacionId != 7), "Numero", "Numero");
            List<string> prioridad = new List<string>
            {
                "Alta",
                "Media",
                "Baja"
            };
            ViewData["Prioridad"] = new SelectList(prioridad);
            return View(incidencia);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Incidencias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var incidencia = await _context.Incidencia.FindAsync(id);
            if (incidencia == null)
            {
                return NotFound();
            }
            var estados = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "1", Value = "Resuelto"},
                    new SelectListItem {Text = "0", Value = "Pendiente"}
                }, "Value", "Text");
            ViewData["Estado"] = new SelectList(estados, estados.DataTextField, estados.DataValueField);
            var encargados = _userManager.GetUsersInRoleAsync("Mantenimiento").Result;
            ViewData["Encargado"] = new SelectList(encargados);
            List<string> prioridad = new List<string>
            {
                "Alta",
                "Media",
                "Baja"
            };
            ViewData["Prioridad"] = new SelectList(prioridad);
            return View(incidencia);
        }

        // POST: Incidencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IncidenciaID,EmpleadoId,Habitacion,Prioridad,Descripcion,Encargado,Estado,FechaAbierto,FechaCerrado")] Incidencia incidencia)
        {
            if (id != incidencia.IncidenciaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    HoteleriaContext _context2 = new HoteleriaContext();
                    HoteleriaContext _context3 = new HoteleriaContext();
                    Incidencia inc = _context2.Incidencia.Find(incidencia.IncidenciaID);
                    
                    if (incidencia.Encargado != null && incidencia.Estado == 0)
                    {
                        Empleado emp = _context2.Empleado.Where(e => e.UsuarioNombre == incidencia.Encargado).First();
                        Habitacion hab = _context2.Habitacion.Where(h => h.Numero == incidencia.Habitacion).First();
                        AsignacionHabitacion asignacionHabitacion = new AsignacionHabitacion
                        {
                            EmpleadoId = emp.EmpleadoId,
                            HabitacionId = hab.HabitacionId,
                            RolId = 4
                        };
                        if (inc.Encargado == null)
                        {
                            _context2.Add(asignacionHabitacion);
                        }
                        else
                        {
                            _context2.Update(asignacionHabitacion);
                        }
                        new NotificacionService().Notificar(emp, hab, ActividadTipo.Mantenimiento);
                    }
                    if (incidencia.Estado == 0)
                    {
                        incidencia.FechaCerrado = null;
                        var habitacion = _context3.Habitacion.Where(a => a.Numero == incidencia.Habitacion).First();
                        if (habitacion.EstadoHabitacionId != 7)
                        {
                            habitacion.EstadoHabitacionId = 7;
                            _context3.Update(habitacion);
                        }
                    }
                    else if(incidencia.Estado == 1)
                    {
                        incidencia.FechaCerrado = DateTime.Now;
                        var habitacion = _context3.Habitacion.Where(a => a.Numero == incidencia.Habitacion).First();
                        var limpiezaid = _context3.AsignacionHabitacion.Where(a => a.HabitacionId == habitacion.HabitacionId && a.RolId == 3).First().EmpleadoId;
                        var emplimp = _context3.Empleado.Find(limpiezaid);
                        habitacion.EstadoHabitacionId = 3;
                        _context3.Update(habitacion);
                        new NotificacionService().Notificar(emplimp, habitacion, ActividadTipo.Limpieza);
                        var emp = _context2.Empleado.Where(e => e.UsuarioNombre == incidencia.Encargado).First();
                        var asignacionHabitacion = _context2.AsignacionHabitacion.Where(a => a.EmpleadoId == emp.EmpleadoId && a.HabitacionId == habitacion.HabitacionId).First();
                        _context2.Remove(asignacionHabitacion);
                    }
                    _context.Update(incidencia);
                    await _context.SaveChangesAsync();
                    await _context2.SaveChangesAsync();
                    await _context3.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IncidenciaExists(incidencia.IncidenciaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                alert("success", "Incidencia editada con exito", "Operacion exitosa");
                return RedirectToAction(nameof(Index));
            }
            var estados = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "1", Value = "Resuelto"},
                    new SelectListItem {Text = "0", Value = "Pendiente"}
                }, "Value", "Text");
            ViewData["Estado"] = new SelectList(estados, estados.DataTextField, estados.DataValueField);
            var encargados = _userManager.GetUsersInRoleAsync("Mantenimiento").Result;
            ViewData["Encargado"] = new SelectList(encargados);
            List<string> prioridad = new List<string>
            {
                "Alta",
                "Media",
                "Baja"
            };
            ViewData["Prioridad"] = new SelectList(prioridad);
            return View(incidencia);
        }

        private bool IncidenciaExists(int id)
        {
            return _context.Incidencia.Any(e => e.IncidenciaID == id);
        }
    }
}
