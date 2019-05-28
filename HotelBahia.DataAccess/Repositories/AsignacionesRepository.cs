using HotelBahia.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBahia.DataAccess.Repositories
{
    public class AsignacionesRepository
    {
        private readonly HoteleriaContext _context;
        public AsignacionesRepository(HoteleriaContext context)
        {
            _context = context;
        }
            
        public AsignacionHabitacion EmpleadoAsignadoPorRol(int idHabitacion, int idRol)
        {
            return _context.AsignacionHabitacion
                .Include(x => x.Empleado)
                    .ThenInclude(x => x.Usuario)
                        .Where(x => x.Empleado.Usuario.RolId == idRol && x.HabitacionId == idHabitacion)
                        .SingleOrDefault();
        }
    }
}
