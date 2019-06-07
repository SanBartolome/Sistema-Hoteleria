using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.BussinesLogic.Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBahia.Presentacion.Web.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareasController : ControllerBase
    {
        private readonly ITareaRepository _tareaRepository;

        public TareasController(ITareaRepository tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        [HttpGet("[action]")]
        public ActionResult ItemsTareas(int idHabitacion, int tipoTarea)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var actividades = _tareaRepository.GetActividadesTarea(idHabitacion);
            if ((ActividadTipo)tipoTarea != ActividadTipo.Supervision)
            {
                actividades = actividades.Where(x => x.Actividad.TipoActividadId == tipoTarea);
            }
            actividades.ToList()
                .ForEach(x => x.Actividad.HabitacionActividad = null);
            return Ok(actividades);
        }

        [HttpPost("[action]")]
        public ActionResult RegistrarSupervision([FromBody]List<HabitacionActividad> habitacionActividades)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            foreach (var item in habitacionActividades)
            {
                _tareaRepository.Edit(item);
            }
            _tareaRepository.SaveChanges();
            return Ok(habitacionActividades);
        }
    }
}