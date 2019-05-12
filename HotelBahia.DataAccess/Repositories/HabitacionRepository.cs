using HotelBahia.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBahia.DataAccess.Repositories
{
    public class HabitacionRepository : Repository<Habitacion> 
    {
        public HabitacionRepository(HoteleriaContext context) : base (context)
        {
            
        }

        public Habitacion BuscarPorNro(int numero)
        {
            return Find(x => x.Numero == numero).First();
        }


    }
}
