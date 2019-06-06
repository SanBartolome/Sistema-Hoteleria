using HotelBahia.BussinesLogic.Domain;
using HotelBahia.DataAccess.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class HabitacionsController : Controller
    {
        private readonly HoteleriaContext _context;

        public HabitacionsController(HoteleriaContext context)
        {
            _context = context;
        }

        // GET: Habitacions
        public async Task<IActionResult> Index()
        {
            var hoteleriaContext = _context.Habitacion.Include(h => h.EstadoHabitacion).Include(h => h.TipoHabitacion);
            return View(await hoteleriaContext.ToListAsync());
        }

        // GET: Habitacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacion
                .Include(h => h.EstadoHabitacion)
                .Include(h => h.TipoHabitacion)
                .FirstOrDefaultAsync(m => m.HabitacionId == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // GET: Habitacions/Create
        public IActionResult Create()
        {
            ViewData["EstadoHabitacion"] = new SelectList(_context.EstadoHabitacion, "EstadoHabitacionId", "EstadoNombre");
            ViewData["TipoHabitacionId"] = new SelectList(_context.TipoHabitacion, "TipoHabitacionId", "TipoHabitacionId");
            return View();
        }

        // POST: Habitacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HabitacionId,Numero,Piso,EstadoHabitacionId,TipoHabitacionId")] Habitacion habitacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(habitacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstadoHabitacion"] = new SelectList(_context.EstadoHabitacion, "EstadoHabitacionId", "EstadoNombre", habitacion.EstadoHabitacionId);
            ViewData["TipoHabitacionId"] = new SelectList(_context.TipoHabitacion, "TipoHabitacionId", "TipoHabitacionId", habitacion.TipoHabitacionId);
            return View(habitacion);
        }

        // GET: Habitacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            ViewData["EstadoHabitacion"] = new SelectList(_context.EstadoHabitacion, "EstadoHabitacionId", "EstadoNombre", habitacion.EstadoHabitacionId);
            ViewData["TipoHabitacionId"] = new SelectList(_context.TipoHabitacion, "TipoHabitacionId", "TipoHabitacionId", habitacion.TipoHabitacionId);
            return View(habitacion);
        }

        // POST: Habitacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HabitacionId,Numero,Piso,EstadoHabitacionId,TipoHabitacionId")] Habitacion habitacion)
        {
            if (id != habitacion.HabitacionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(habitacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HabitacionExists(habitacion.HabitacionId))
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
            ViewData["EstadoHabitacion"] = new SelectList(_context.EstadoHabitacion, "EstadoHabitacionId", "EstadoNombre", habitacion.EstadoHabitacionId);
            ViewData["TipoHabitacionId"] = new SelectList(_context.TipoHabitacion, "TipoHabitacionId", "TipoHabitacionId", habitacion.TipoHabitacionId);
            return View(habitacion);
        }

        // GET: Habitacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var habitacion = await _context.Habitacion
                .Include(h => h.EstadoHabitacion)
                .Include(h => h.TipoHabitacion)
                .FirstOrDefaultAsync(m => m.HabitacionId == id);
            if (habitacion == null)
            {
                return NotFound();
            }

            return View(habitacion);
        }

        // POST: Habitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var habitacion = await _context.Habitacion.FindAsync(id);
            _context.Habitacion.Remove(habitacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HabitacionExists(int id)
        {
            return _context.Habitacion.Any(e => e.HabitacionId == id);
        }
    }
}
