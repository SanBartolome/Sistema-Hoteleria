using HotelBahia.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.DataAccess.Repositories
{
   public class HabitacionRepository
    {

        private readonly HotelDbContext _context;
        public HabitacionRepository(HotelDbContext context) {
            _context = context;
        }
        //public async List<Habitacion> listhabitaciones() {

        //  await  _context.Habitacion
        //    return;
        //}
    }
}
