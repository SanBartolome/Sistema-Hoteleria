using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.DataAccess.Context;
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

        public HabitacionController(IHabitacionRepository habitacionRepository, HoteleriaContext context)
        {
            _context = context;
            _habitacionRepository = habitacionRepository;
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
            if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.Ocupado)
                return BadRequest("El estado actual debe ser Ocupado");

            habitacion.EstadoHabitacionId = (int)HabitacionEstado.Desocupado;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
            //var empleado = new AsignacionesService(_asignacionesRepository).EmpleadoAsignadoPorRol(habitacion.HabitacionId, (int)RolEnum.AgenteDeLimpieza);
            //new NotificacionService().Notificar(empleado, habitacion, ActividadTipo.Limpieza);
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
            if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.Desocupado && habitacion.EstadoHabitacionId != (int)HabitacionEstado.LimpiezaIncompleta)
                return BadRequest("No se puede realizar limpieza en esta habitacion");

            habitacion.EstadoHabitacionId = (int)HabitacionEstado.LimpiezaRealizada;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
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
            if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.LimpiezaRealizada)
                return BadRequest("No se puede realizar limpieza en esta habitacion");

            habitacion.EstadoHabitacionId = (int)HabitacionEstado.LimpiezaIncompleta;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
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
                return BadRequest("No se puede habilitar esta habitacion");

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
                return BadRequest("No se puede Ocupar esta habitacion");

            habitacion.EstadoHabitacionId = (int)HabitacionEstado.Ocupado;
            _habitacionRepository.Edit(habitacion);
            _habitacionRepository.SaveChanges();
            return Ok(habitacion);
        }


        private bool HabitacionExists(int id)
        {
            return _context.Habitacion.Any(e => e.HabitacionId == id);
        }
    }
}