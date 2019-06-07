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
        public AsignacionesController(IAsignacionesRepository asignacionesRepository)
        {
            _asignacionesRepository = asignacionesRepository;
        }
        [HttpGet("[action]/{idEmpleado}")]
        public ActionResult HabitacionesAsignadas([FromRoute] int idEmpleado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var habitacionesAsignadas = _asignacionesRepository.HabitacionesAsignadas(idEmpleado);
            if (habitacionesAsignadas == null)
            {
                return NotFound();
            }
            
            return Ok(habitacionesAsignadas);

        }
    }
}