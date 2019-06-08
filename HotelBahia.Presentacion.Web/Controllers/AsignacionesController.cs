using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.DataAccess.Context;
using System.Data.SqlClient;
using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.BussinesLogic.Dto;

namespace HotelBahia.Presentacion.Web.Controllers
{
    
    public class AsignacionesController : Controller
    {
        private readonly HoteleriaContext _context;

        public AsignacionesController(HoteleriaContext context)
        {
            _context = context;
        }

        // GET: Asignaciones
        [Route("[controller]/Limpieza")]
        public async Task<IActionResult> Index()
        {
            var hoteleriaContext = _context.AsignacionHabitacion.Include(a => a.Empleado).Include(a => a.Habitacion);
            var lista = await hoteleriaContext.ToListAsync();
            return View(lista.Where( x => x.RolId == 3));
        }

        // GET: Asignaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionHabitacion = await _context.AsignacionHabitacion
                .Include(a => a.Empleado)
                .Include(a => a.Habitacion)
                .FirstOrDefaultAsync(m => m.AsignacionHabitacionId == id);
            if (asignacionHabitacion == null)
            {
                return NotFound();
            }

            return View(asignacionHabitacion);
        }

        // GET: Asignaciones/Create
        public IActionResult Create()
        {
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdRol", (int)RolEnum.AgenteDeLimpieza)
            };
            var result = _context.Empleado.FromSql("EmpleadosPorRol @IdRol", parametros)
                .Select(x => new { Texto = x.Nombres + " " + x.Apellidos, x.EmpleadoId });

            ViewData["EmpleadoId"] = new SelectList(result, "EmpleadoId", "Texto");

            var habitaciones = _context.Habitacion.Include(a => a.AsignacionHabitacion).Where(a => a.AsignacionHabitacion.Count == 0 || !a.AsignacionHabitacion.Any(x => x.RolId == (int)RolEnum.AgenteDeLimpieza));


            ViewData["HabitacionId"] = new SelectList(habitaciones, "HabitacionId", "Numero");
            return View();
        }

        // POST: Asignaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AsignacionHabitacionId,EmpleadoId,HabitacionId,RolId,Fecha")] AsignacionHabitacion asignacionHabitacion)
        {
            if (ModelState.IsValid)
            {
                asignacionHabitacion.RolId = (int)RolEnum.AgenteDeLimpieza;
                _context.Add(asignacionHabitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdRol", (int)RolEnum.AgenteDeLimpieza)
            };
            var result = _context.Empleado.FromSql("EmpleadosPorRol @IdRol", parametros)
                .Select(x => new { Texto = x.Nombres + " " + x.Apellidos, x.EmpleadoId });

            ViewData["EmpleadoId"] = new SelectList(result, "EmpleadoId", "Texto", asignacionHabitacion.EmpleadoId);
            ViewData["HabitacionId"] = new SelectList(_context.Habitacion, "HabitacionId", "Numero", asignacionHabitacion.HabitacionId);
            return View(asignacionHabitacion);
        }

        // GET: Asignaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionHabitacion = await _context.AsignacionHabitacion.FindAsync(id);
            if (asignacionHabitacion == null)
            {
                return NotFound();
            }

            SqlParameter[] parametros = new SqlParameter[]
            {   
                new SqlParameter("@IdRol", (int)RolEnum.AgenteDeLimpieza)
            };
            var result = _context.Empleado.FromSql("EmpleadosPorRol @IdRol", parametros)
                .Select(x => new { Texto = x.Nombres + " "+   x.Apellidos, x.EmpleadoId});

            ViewData["EmpleadoId"] = new SelectList(result, "EmpleadoId", "Texto", asignacionHabitacion.EmpleadoId);
            ViewData["HabitacionId"] = new SelectList(_context.Habitacion.Where(x => x.HabitacionId == asignacionHabitacion.HabitacionId), "HabitacionId", "Numero", asignacionHabitacion.HabitacionId);
            return View(asignacionHabitacion);
        }

        // POST: Asignaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AsignacionHabitacionId,EmpleadoId,HabitacionId,RolId,Fecha")] AsignacionHabitacion asignacionHabitacion)
        {
            if (id != asignacionHabitacion.AsignacionHabitacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(asignacionHabitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AsignacionHabitacionExists(asignacionHabitacion.AsignacionHabitacionId))
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
            SqlParameter[] parametros = new SqlParameter[]
            {
                new SqlParameter("@IdRol", (int)RolEnum.AgenteDeLimpieza)
            };
            var result = _context.Empleado.FromSql("EmpleadosPorRol @IdRol", parametros)
                .Select(x => new { Texto = x.Nombres + " " + x.Apellidos, x.EmpleadoId });

            ViewData["EmpleadoId"] = new SelectList(result, "EmpleadoId", "Texto", asignacionHabitacion.EmpleadoId);
            ViewData["HabitacionId"] = new SelectList(_context.Habitacion.Where(x => x.HabitacionId == asignacionHabitacion.HabitacionId), "HabitacionId", "Numero", asignacionHabitacion.HabitacionId);
            return View(asignacionHabitacion);
        }

        // GET: Asignaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var asignacionHabitacion = await _context.AsignacionHabitacion
                .Include(a => a.Empleado)
                .Include(a => a.Habitacion)
                .FirstOrDefaultAsync(m => m.AsignacionHabitacionId == id);
            if (asignacionHabitacion == null)
            {
                return NotFound();
            }

            return View(asignacionHabitacion);
        }

        // POST: Asignaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var asignacionHabitacion = await _context.AsignacionHabitacion.FindAsync(id);
            _context.AsignacionHabitacion.Remove(asignacionHabitacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AsignacionHabitacionExists(int id)
        {
            return _context.AsignacionHabitacion.Any(e => e.AsignacionHabitacionId == id);
        }
    }
}
