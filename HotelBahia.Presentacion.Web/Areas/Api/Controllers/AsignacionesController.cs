using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Contracts.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBahia.Presentacion.Web.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly IAsignacionesRepository _asignacionesRepository;
        private readonly IEmpleadoRepository _empleadoRepository;
        public AsignacionesController(IAsignacionesRepository asignacionesRepository, IEmpleadoRepository empleadoRepository)
        {
            _asignacionesRepository = asignacionesRepository;
            _empleadoRepository = empleadoRepository;
        }
        [HttpGet("[action]/{idEmpleado}")]
        public ActionResult HabitacionesAsignadas([FromRoute] int idEmpleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.Identity.Name;
            var empleado = _empleadoRepository.Find(x => x.UsuarioNombre == username).SingleOrDefault();

            Response.Headers.Add("Cache-Control", " no-cache ");

            var habitacionesAsignadas = _asignacionesRepository.HabitacionesAsignadas(empleado.EmpleadoId);
            if (habitacionesAsignadas == null)
            {
                return NotFound();
            }
            
            return Ok(habitacionesAsignadas);

        }

        //[HttpGet("[action]/{idEmpleado}")]
        //public ActionResult HabitacionesAsignadas([FromRoute] int idEmpleado)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    1 /*var username = User.Identity.Name;
        //    var empleado = _empleadoRepository.Find(x => x.UsuarioNombre == username).SingleOrDefault();

        //    Response.Headers.Add("Cache-Control", " no-cache ");
        //
        //    RealizarMantenimientoViewModel model = new RealizarMantenimientoViewModel();
        //    model.Incidencias = _context.Incidencia.Where(i => i.Encargado == username).Where(f => f.Estado == 0).ToList();
        //    List<Habitacion> lista = new List<Habitacion>();
        //    foreach(var incidencia in model.Incidencias)
        //    {
        //        var hab = _context.Habitacion.Where(h => h.Numero == incidencia.Habitacion).SingleOrDefault();
        //        lista.Add(hab);
        //    }
        //    model.Habitaciones = lista;
        //    if (model == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(model);

        //}
    }
}