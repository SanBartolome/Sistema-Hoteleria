﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.DataAccess.Context;
using HotelBahia.Presentacion.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class EmpleadoesController : Controller
    {
        private readonly HoteleriaContext _context;
        private readonly UserManager<UserLogin> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        

        public EmpleadoesController(HoteleriaContext context, UserManager<UserLogin> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Empleadoes
        public async Task<IActionResult> Index()
        {
            var hoteleriaContext = _context.Empleado;
            return View(await hoteleriaContext.ToListAsync());
        }

        // GET: Empleadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            var usuario = await _context.Usuario
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.UsuarioNombre == empleado.UsuarioNombre);
            if (empleado == null)
            {
                return NotFound();
            }
            if (usuario == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleadoes/Create
        public IActionResult Create()
        {
            ViewData["Rol"] = new SelectList(_context.Rol, "RolId", "Nombre");
            return View();
        }

        // POST: Empleadoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmpleadoViewModel empleado)
        {

            var user = new UserLogin { UserName = empleado.Empleado.UsuarioNombre, Email = empleado.Empleado.Correo };
            var result = await _userManager.CreateAsync(user, empleado.Usuario.Password);
            
            user = await _userManager.FindByNameAsync(user.UserName);
            var role = await _roleManager.FindByIdAsync(empleado.Usuario.RolId.ToString());
            await _userManager.AddToRoleAsync(user, role.Name);

            if (result.Succeeded)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Empleadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id);
            var usuario = await _context.Usuario.FindAsync(empleado.UsuarioNombre);
            if (empleado == null)
            {
                return NotFound();
            }
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["Rol"] = new SelectList(_context.Rol, "RolId", "Nombre", usuario.RolId);
            return View(empleado);
        }

        // POST: Empleadoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpleadoId,Nombres,Apellidos,Direccion,Telefono,Correo,Sexo,UsuarioNombre")] Empleado empleado, [Bind("UsuarioNombre,Password,RolId")] Usuario usuario)
        {
            if (id != empleado.EmpleadoId)
            {
                return NotFound();
            }

            if (usuario.UsuarioNombre == null)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    _context.Update(usuario);
                    empleado.UsuarioNombre = usuario.UsuarioNombre;
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.EmpleadoId))
                    {
                        return NotFound();
                    }
                    else if (!UsuarioExists(usuario.UsuarioNombre))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            /*}
            ViewData["Rol"] = new SelectList(_context.Rol, "RolId", "Nombre", usuario.RolId);
            return View(empleado);*/
        }

        // GET: Empleadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .FirstOrDefaultAsync(m => m.EmpleadoId == id);
            var usuario = await _context.Usuario
                .Include(u => u.Rol)
                .FirstOrDefaultAsync(m => m.UsuarioNombre == empleado.UsuarioNombre);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleado.FindAsync(id);
            var usuario = await _context.Usuario.FindAsync(empleado.UsuarioNombre);
            _context.Empleado.Remove(empleado);
            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleado.Any(e => e.EmpleadoId == id);
        }

        private bool UsuarioExists(string usuarioNombre)
        {
            return _context.Usuario.Any(u => u.UsuarioNombre == usuarioNombre);
        }
    }
}
