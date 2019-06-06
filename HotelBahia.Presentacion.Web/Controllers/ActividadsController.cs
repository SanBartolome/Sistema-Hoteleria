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
    public class ActividadsController : Controller
    {
        private readonly HoteleriaContext _context;

        public ActividadsController(HoteleriaContext context)
        {
            _context = context;
        }

        // GET: Actividads
        public async Task<IActionResult> Index()
        {
            var hoteleriaContext = _context.Actividad.Include(a => a.TipoActividad);
            return View(await hoteleriaContext.ToListAsync());
        }

        // GET: Actividads/Create
        public IActionResult Create()
        {
            ViewData["TipoActividad"] = new SelectList(_context.TipoActividad, "TipoActividadId", "Nombre");
            List<String> estados = new List<string>
            {
                "Activo",
                "Inactivo"
            };
            ViewData["Estado"] = new SelectList(estados);
            return View();
        }

        // POST: Actividads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActividadId,TipoActividadId,Descripcion,Estado")] Actividad actividad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(actividad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TipoActividad"] = new SelectList(_context.TipoActividad, "TipoActividadId", "Nombre", actividad.TipoActividadId);
            List<String> estados = new List<string>
            {
                "Activo",
                "Inactivo"
            };
            ViewData["Estado"] = new SelectList(estados);
            return View(actividad);
        }

        // GET: Actividads/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividad.FindAsync(id);
            if (actividad == null)
            {
                return NotFound();
            }
            ViewData["TipoActividad"] = new SelectList(_context.TipoActividad, "TipoActividadId", "Nombre", actividad.TipoActividadId);
            List<String> estados = new List<string>
            {
                "Activo",
                "Inactivo"
            };
            ViewData["Estado"] = new SelectList(estados);
            return View(actividad);
        }

        // POST: Actividads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActividadId,TipoActividadId,Descripcion,Estado")] Actividad actividad)
        {
            if (id != actividad.ActividadId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(actividad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ActividadExists(actividad.ActividadId))
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
            ViewData["TipoActividad"] = new SelectList(_context.TipoActividad, "TipoActividadId", "Nombre", actividad.TipoActividadId);
            List<String> estados = new List<string>
            {
                "Activo",
                "Inactivo"
            };
            ViewData["Estado"] = new SelectList(estados);
            return View(actividad);
        }

        // GET: Actividads/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var actividad = await _context.Actividad
                .Include(a => a.TipoActividad)
                .FirstOrDefaultAsync(m => m.ActividadId == id);
            if (actividad == null)
            {
                return NotFound();
            }

            return View(actividad);
        }

        // POST: Actividads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var actividad = await _context.Actividad.FindAsync(id);
            _context.Actividad.Remove(actividad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ActividadExists(int id)
        {
            return _context.Actividad.Any(e => e.ActividadId == id);
        }
    }
}
