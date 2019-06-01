using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.BussinesLogic.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelBahia.Presentacion.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AsignacionesController : ControllerBase
    {
        private readonly AsignacionesService _asignacionesService;
        public AsignacionesController(AsignacionesService asignacionesService)
        {
            _asignacionesService = asignacionesService;
        }

        [HttpGet("[action]")]
        public ActionResult HabitacionesAsignadas(int idEmpleado)
        {
            var response = _asignacionesService.HabitacionesAsignadas(idEmpleado)
                .Select(x => new {
                    x.Numero,
                    x.HabitacionId,
                    x.EstadoHabitacionId,
                    Estado = Enum.GetName(typeof(HabitacionEstado), x.EstadoHabitacionId),
                }) ;
            return Ok(response);
        }
    }
}