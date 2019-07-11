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
using Microsoft.AspNetCore.Authorization;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class EvaluacionesController : BaseController
    {
        private readonly HoteleriaContext _context;
        private readonly UserManager<UserLogin> _userManager;

        public EvaluacionesController(HoteleriaContext context, UserManager<UserLogin> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [Authorize(Roles = "Administrador")]
        // GET: Evaluaciones
        public IActionResult Index()
        {
            List<EvaluacionModel> models = new List<EvaluacionModel>();
            var cantidad = _context.EvaluacionSupervisor.Include(es => es.ResultadoEvaluacion).Include(es => es.Empleado).ToList().Count;
            if (cantidad > 0)
            {
                for (int i = 1; i <= cantidad; i++)
                {
                    EvaluacionModel model = new EvaluacionModel();
                    model.EvaluacionSupervisor = _context.EvaluacionSupervisor.Find(i);
                    var empleado = _context.Empleado.Where(e => e.EmpleadoId == model.EvaluacionSupervisor.EmpleadoId).FirstOrDefault();
                    model.Empleado = empleado;
                    var resultadoEvaluacion = _context.ResultadoEvaluacion.Where(e => e.ResultadoEvaluacionId == model.EvaluacionSupervisor.ResultadoEvaluacionId).FirstOrDefault();
                    model.ResultadoEvaluacion = resultadoEvaluacion;
                    models.Add(model);
                }
            }
            return View(models);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Evaluaciones/Details/5
        public IActionResult Details(int? id)
        {
            EvaluacionModel model = new EvaluacionModel();
            if (id == null)
            {
                return NotFound();
            }

            var evaluacionSupervisor = _context.EvaluacionSupervisor.Where(m => m.EvaluacionSupervisorId == id).FirstOrDefault();
            var resultadoEvaluacion = _context.ResultadoEvaluacion
                .Where(m => m.ResultadoEvaluacionId == evaluacionSupervisor.ResultadoEvaluacionId).FirstOrDefault();
            var empleado = _context.Empleado
                .Where(m => m.EmpleadoId == evaluacionSupervisor.EmpleadoId).FirstOrDefault();
            if (resultadoEvaluacion == null)
            {
                return NotFound();
            }
            if (empleado == null)
            {
                return NotFound();
            }
            model.ResultadoEvaluacion = resultadoEvaluacion;
            model.Empleado = empleado;
            return View(model);
        }

        [Authorize(Roles = "Administrador")]
        // GET: Evaluaciones/Create
        public IActionResult Create()
        {
            var supervisores = _userManager.GetUsersInRoleAsync("Supervisor").Result;
            ViewData["Supervisores"] = new SelectList(supervisores);
            return View();
        }

        // POST: Evaluaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EvaluacionModel evaluacionModel)
        {
            /*if (ModelState.IsValid)
            {*/
                HoteleriaContext _context2 = new HoteleriaContext();
                ResultadoEvaluacion resultadoEvaluacion = new ResultadoEvaluacion();
                Empleado empleado = new Empleado();
                var emp = await _context.Empleado.FirstOrDefaultAsync(e => e.UsuarioNombre == _userManager.GetUserName(User));
                resultadoEvaluacion = evaluacionModel.ResultadoEvaluacion;
                resultadoEvaluacion.Evaluador = emp.UsuarioNombre;
                _context.Add(resultadoEvaluacion);
                await _context.SaveChangesAsync();
                var supervisorid = _context2.Empleado.Where(e => e.UsuarioNombre == evaluacionModel.Empleado.UsuarioNombre).First().EmpleadoId;
                var evaluacionSupervisor = new EvaluacionSupervisor { EmpleadoId = supervisorid, ResultadoEvaluacionId = resultadoEvaluacion.ResultadoEvaluacionId };
                _context2.Add(evaluacionSupervisor);
                await _context2.SaveChangesAsync();
            alert("success", "Evaluacion registrada con exito", "Operacion exitosa");
            return RedirectToAction(nameof(Index));
            /*}
            var supervisores = _userManager.GetUsersInRoleAsync("Supervisor").Result;
            ViewData["Supervisores"] = new SelectList(supervisores);
            return View(evaluacionModel);*/
        }

    }
}
