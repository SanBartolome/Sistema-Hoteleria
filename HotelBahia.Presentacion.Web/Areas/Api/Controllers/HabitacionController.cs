using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.BussinesLogic.Servicios.AppServices;
using HotelBahia.DataAccess.Context;
using HotelBahia.Presentacion.Web.Models.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {
        private readonly HoteleriaContext _context;
        private readonly IHabitacionRepository _habitacionRepository;
        private readonly IAsignacionesRepository _asignacionesRepository;

        public HabitacionController(IHabitacionRepository habitacionRepository,
            IAsignacionesRepository asignacionesRepository,
            HoteleriaContext context)
        {
            _context = context;
            _habitacionRepository = habitacionRepository;
            _asignacionesRepository = asignacionesRepository;
        }

        // GET: api/Habitacion
        [HttpGet]
        public IEnumerable<Habitacion> GetHabitacion()
        {
            return _context.Habitacion.Include(x => x.EstadoHabitacion);
        }

        // GET: api/Habitacion/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHabitacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var habitacion = await _context.Habitacion.FindAsync(id);

            if (habitacion == null)
            {
                return NotFound();
            }

            return Ok(habitacion);
        }

        // PUT: api/Habitacion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHabitacion([FromRoute] int id, [FromBody] Habitacion habitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != habitacion.HabitacionId)
            {
                return BadRequest();
            }

            _context.Entry(habitacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabitacionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Habitacion
        [HttpPost]
        public async Task<IActionResult> PostHabitacion([FromBody] Habitacion habitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Habitacion.Add(habitacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHabitacion", new { id = habitacion.HabitacionId }, habitacion);
        }

        // DELETE: api/Habitacion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHabitacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }

            _context.Habitacion.Remove(habitacion);
            await _context.SaveChangesAsync();

            return Ok(habitacion);
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> CheckOut([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            var asigEmpleado = _asignacionesRepository.EmpleadoAsignadoPorRol(habitacion.HabitacionId, (int)RolEnum.AgenteDeLimpieza);
            if ( asigEmpleado == null )
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.MISSING_CLEANER" } });
            }
            if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.Ocupado)
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.ERROR_ON_CHECKOUT" } });
            }
            habitacion.EstadoHabitacionId = (int)HabitacionEstado.Desocupado;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
            new NotificacionService().Notificar(asigEmpleado.Empleado, habitacion, ActividadTipo.Limpieza);
            return Ok(habitacion);

        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> CheckLimpiezaRealizada([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            var asignSupervisor = _asignacionesRepository.EmpleadoAsignadoPorRol(habitacion.HabitacionId, (int)RolEnum.Supervisor);
            if (asignSupervisor == null)
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.MISSING_SUPERVISOR" } });
            }
            if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.Desocupado && habitacion.EstadoHabitacionId != (int)HabitacionEstado.LimpiezaIncompleta)
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.ERROR_ON_CHECK_CLEAN" } });
            }
            habitacion.EstadoHabitacionId = (int)HabitacionEstado.LimpiezaRealizada;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
            new NotificacionService().Notificar(asignSupervisor.Empleado, habitacion, ActividadTipo.Supervision);
            return Ok(habitacion);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> CheckLimpiezaIncompleta([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            var asigEmpleado = _asignacionesRepository.EmpleadoAsignadoPorRol(habitacion.HabitacionId, (int)RolEnum.AgenteDeLimpieza);
            if (asigEmpleado == null)
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.MISSING_CLEANER" } });
            }
            if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.LimpiezaRealizada)
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.ERROR_ON_CHECK_SUPERVISE" } });
            }

            habitacion.EstadoHabitacionId = (int)HabitacionEstado.LimpiezaIncompleta;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
            new NotificacionService().Notificar(asigEmpleado.Empleado, habitacion, ActividadTipo.Limpieza);
            return Ok(habitacion);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> HabilitarHabitacion([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.LimpiezaRealizada)
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.ERROR_ON_CHECK_SUPERVISE" } });
            }

            habitacion.EstadoHabitacionId = (int)HabitacionEstado.Disponible;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
            return Ok(habitacion);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> CheckIn([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.Disponible)
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.ERROR_ON_CHECKIN" } });
            }

            habitacion.EstadoHabitacionId = (int)HabitacionEstado.Ocupado;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
            return Ok(habitacion);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult> CheckMantenimientoRealizado([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var habitacion = await _context.Habitacion.FindAsync(id);
            if (habitacion == null)
            {
                return NotFound();
            }
            var asignEmpleado = _asignacionesRepository.EmpleadoAsignadoPorRol(habitacion.HabitacionId, (int)RolEnum.AgenteDeLimpieza);
            if (asignEmpleado == null)
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.MISSING_CLEANER" } });
            }
            if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.Bloqueado)
            {
                return BadRequest(new ErrorResponse() { messages = new string[] { "HABITACION.ERROR_ON_CHECK_UPKEEP" } });
            }
            habitacion.EstadoHabitacionId = (int)HabitacionEstado.Desocupado;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
            new NotificacionService().Notificar(asignEmpleado.Empleado, habitacion, ActividadTipo.Limpieza);
            return Ok(habitacion);
        }

        private bool HabitacionExists(int id)
        {
            return _context.Habitacion.Any(e => e.HabitacionId == id);
        }
    }
}