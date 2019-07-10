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
    public class ObjetosPerdidosController : BaseController
    {
        private readonly HoteleriaContext _context;
        private readonly UserManager<UserLogin> _userManager;

        public ObjetosPerdidosController(HoteleriaContext context, UserManager<UserLogin> userManager)
        {
            _context = context;
            _userManager = userManager;
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
        public IActionResult Create([FromQuery(Name = "habitacion")] string habitacionNum)
        {
            if (User.IsInRole("Limpieza"))
            {
                var habitacion = _context.Habitacion.FirstOrDefault(h => h.Numero == int.Parse(habitacionNum));
                if (habitacion == null)
                {
                    return View();
                }
                return View(new ObjetoPerdido() { Habitacion = habitacion.Numero });
            }
            ViewData["Habitaciones"] = new SelectList(_context.Habitacion, "Numero", "Numero");
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
                var empleado = await _context.Empleado.FirstOrDefaultAsync(e => e.UsuarioNombre == _userManager.GetUserName(User));
                objetoPerdido.EmpleadoId = empleado.EmpleadoId;
                objetoPerdido.Fecha = DateTime.Now;
                objetoPerdido.Estado = 0;
                _context.Add(objetoPerdido);
                await _context.SaveChangesAsync();
                alert("success", "Objeto Perdido registrado con éxito", "Operación exitosa");
                if (User.IsInRole("Limpieza"))
                {
                    return RedirectToAction("index", "limpieza");
                }
                if (User.IsInRole("Administrador"))
                {
                    return RedirectToAction("index");
                }
            }
            ViewData["Habitaciones"] = new SelectList(_context.Habitacion, "Numero", "Numero");
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
                alert("success", "Detalles del objeto perdido editado con exito", "Operacion exitosa");
                return RedirectToAction(nameof(Index));
            }
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
