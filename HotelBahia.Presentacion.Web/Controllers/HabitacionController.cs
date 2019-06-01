using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBahia.BussinesLogic.Contracts.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelBahia.Presentacion.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitacionController : ControllerBase
    {
        private IHabitacionRepository _habitacionRepository;
        public HabitacionController(IHabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }
        // GET: api/Habitacion
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(_habitacionRepository.GetAllComplete());
        }

        // GET: api/Habitacion/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Habitacion
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Habitacion/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
