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

        // GET: Incidencias
        public async Task<IActionResult> Index()
        {
            var hoteleriaContext = _context.Incidencia.Include(i => i.Empleado);
            return View(await hoteleriaContext.ToListAsync());
        }

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

        // GET: Incidencias/Create
        public async Task<IActionResult> Create([FromQuery(Name = "habitacion")] string habitacionNum)
        {
            if( User.IsInRole("Supervisor"))
            {
                var habitacion = _context.Habitacion.FirstOrDefault(h => h.Numero == int.Parse(habitacionNum) );
                if( habitacion == null )
                {
                    return View();
                }
                return View( new Incidencia() { Habitacion = habitacion.Numero } );
            }
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Apellidos", incidencia.EmpleadoId);
            return View(incidencia);
        }

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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Apellidos", incidencia.EmpleadoId);
            var estados = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "1", Value = "Resuelto"},
                    new SelectListItem {Text = "0", Value = "Pendiente"}
                }, "Value", "Text");
            ViewData["Estado"] = new SelectList(estados, estados.DataTextField, estados.DataValueField);
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
                    _context.Update(incidencia);
                    if(incidencia.Estado == 0)
                    {
                        var habitacion = _context.Habitacion.Where(a => a.Numero == incidencia.Habitacion).FirstOrDefault();
                        habitacion.EstadoHabitacionId = 7;
                        _context.Update(habitacion);
                    }
                    else if(incidencia.Estado == 1)
                    {
                        var habitacion = _context.Habitacion.Where(a => a.Numero == incidencia.Habitacion).FirstOrDefault();
                        habitacion.EstadoHabitacionId = 3;
                        _context.Update(habitacion);
                    }
                    await _context.SaveChangesAsync();
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Apellidos", incidencia.EmpleadoId);
            var estados = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "1", Value = "Resuelto"},
                    new SelectListItem {Text = "0", Value = "Pendiente"}
                }, "Value", "Text");
            ViewData["Estado"] = new SelectList(estados, estados.DataTextField, estados.DataValueField);
            return View(incidencia);
        }

        private bool IncidenciaExists(int id)
        {
            return _context.Incidencia.Any(e => e.IncidenciaID == id);
        }
    }
}
