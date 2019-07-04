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
    public class EvaluacionesController : Controller
    {
        private readonly HoteleriaContext _context;

        public EvaluacionesController(HoteleriaContext context)
        {
            _context = context;
        }

        // GET: Evaluaciones
        public async Task<IActionResult> Index()
        {
            return View(await _context.EvaluacionSupervisor.ToListAsync());
        }

        // GET: Evaluaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evaluacionSupervisor = await _context.EvaluacionSupervisor
                .Include(a => a.Empleado)
                .Include(a => a.ResultadoEvaluacion)
                .FirstOrDefaultAsync(m => m.ResultadoEvaluacionId == id);
            if (evaluacionSupervisor == null)
            {
                return NotFound();
            }

            return View(evaluacionSupervisor);
        }

        // GET: Evaluaciones/Create
        public IActionResult Create()
        {
            ViewData["Empleado"] = new SelectList(_context.Empleado, "EmpleadoId", "UsuarioNombre");
            return View();
        }

        // POST: Evaluaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultadoEvaluacionId,Tardanzas,Faltas,SupervisionesNegativas,Valoracion,Comentarios,Evaluador")] ResultadoEvaluacion resultadoEvaluacion, int empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(resultadoEvaluacion);
                var evaluacionSupervisor = new EvaluacionSupervisor { EmpleadoId = empleado, ResultadoEvaluacionId = resultadoEvaluacion.ResultadoEvaluacionId };
                _context.Add(evaluacionSupervisor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Empleado"] = new SelectList(_context.Empleado, "EmpleadoId", "UsuarioNombre");
            return View(resultadoEvaluacion);
        }

    }
}
