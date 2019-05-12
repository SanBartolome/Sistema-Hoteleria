using HotelBahia.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
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
            return _context.Habitacion
                .Include(x => x.EstadoHabitacion)
                .Where(x => x.Numero == numero)
                .FirstOrDefault();
        }

        public void EditarEstado(Habitacion habitacion, string estadoNombre)
        {
            var estado =_context.EstadoHabitacion.Where(x => x.EstadoNombre == estadoNombre).FirstOrDefault();
            habitacion.EstadoHabitacion = estado;
            Edit(habitacion);
        }
    }
}
