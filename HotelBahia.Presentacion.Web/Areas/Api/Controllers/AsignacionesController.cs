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

            var habitacionesAsignadas = _asignacionesRepository.HabitacionesAsignadas(empleado.EmpleadoId);
            if (habitacionesAsignadas == null)
            {
                return NotFound();
            }
            
            return Ok(habitacionesAsignadas);

        }
    }
}