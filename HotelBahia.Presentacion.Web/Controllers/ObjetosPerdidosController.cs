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
    public class ObjetosPerdidosController : Controller
    {
        private readonly HoteleriaContext _context;

        public ObjetosPerdidosController(HoteleriaContext context)
        {
            _context = context;
        }

        // GET: ObjetosPerdidos
        public async Task<IActionResult> Index()
        {
            var hoteleriaContext = _context.ObjetoPerdido.Include(o => o.Empleado);
            return View(await hoteleriaContext.ToListAsync());
        }

        // GET: ObjetosPerdidos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objetoPerdido = await _context.ObjetoPerdido
                .Include(o => o.Empleado)
                .FirstOrDefaultAsync(m => m.ObjetoPerdidoId == id);
            if (objetoPerdido == null)
            {
                return NotFound();
            }

            return View(objetoPerdido);
        }

        // GET: ObjetosPerdidos/Create
        public IActionResult Create()
        {
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Apellidos");
            return View();
        }

        // POST: ObjetosPerdidos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ObjetoPerdidoId,EmpleadoId,Nombre,Descripcion,Habitacion,Fecha,Estado")] ObjetoPerdido objetoPerdido)
        {
            if (ModelState.IsValid)
            {
                objetoPerdido.Estado = 0;
                _context.Add(objetoPerdido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Apellidos", objetoPerdido.EmpleadoId);
            return View(objetoPerdido);
        }

        // GET: ObjetosPerdidos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var objetoPerdido = await _context.ObjetoPerdido.FindAsync(id);
            if (objetoPerdido == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Apellidos", objetoPerdido.EmpleadoId);
            var estados = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "1", Value = "Devuelto"},
                    new SelectListItem {Text = "0", Value = "Pendiente"}
                }, "Value", "Text");
            ViewData["Estado"] = new SelectList(estados, estados.DataTextField, estados.DataValueField);
            return View(objetoPerdido);
        }

        // POST: ObjetosPerdidos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ObjetoPerdidoId,EmpleadoId,Nombre,Descripcion,Habitacion,Fecha,Estado")] ObjetoPerdido objetoPerdido)
        {
            if (id != objetoPerdido.ObjetoPerdidoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(objetoPerdido);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ObjetoPerdidoExists(objetoPerdido.ObjetoPerdidoId))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Empleado, "EmpleadoId", "Apellidos", objetoPerdido.EmpleadoId);
            var estados = new SelectList(
                new List<SelectListItem>
                {
                    new SelectListItem {Text = "1", Value = "Devuelto"},
                    new SelectListItem {Text = "0", Value = "Pendiente"}
                }, "Value", "Text");
            ViewData["Estado"] = new SelectList(estados, estados.DataTextField, estados.DataValueField);
            return View(objetoPerdido);
        }

        private bool ObjetoPerdidoExists(int id)
        {
            return _context.ObjetoPerdido.Any(e => e.ObjetoPerdidoId == id);
        }
    }
}
