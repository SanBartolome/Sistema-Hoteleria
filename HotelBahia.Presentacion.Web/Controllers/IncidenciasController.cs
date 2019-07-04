using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.DataAccess.Context;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class IncidenciasController : Controller
    {
        private readonly HoteleriaContext _context;

        public IncidenciasController(HoteleriaContext context)
        {
            _context = context;
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
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Apellidos");
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
                incidencia.Estado = 0;
                _context.Add(incidencia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
