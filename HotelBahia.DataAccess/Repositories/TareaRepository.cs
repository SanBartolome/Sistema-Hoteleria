using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBahia.DataAccess.Repositories
{
    public class TareaRepository : Repository<HabitacionActividad>, ITareaRepository
    {
        public TareaRepository(HoteleriaContext context): base(context)
        {

        }


        public IEnumerable<HabitacionActividad> GetActividadesTarea(int idHabitacion)
        {
            return _context.HabitacionActividad
                 .Include(x => x.Actividad)
                 .Where(x => x.HabitacionId == idHabitacion);
        }
    }
}
