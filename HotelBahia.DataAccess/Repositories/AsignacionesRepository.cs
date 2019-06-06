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
    public class AsignacionesRepository : IAsignacionesRepository
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

        public IEnumerable<Habitacion> HabitacionesAsignadas(int idEmpleado)
        {
            return _context.AsignacionHabitacion
                .Include(x => x.Habitacion)
                .ThenInclude(x => x.EstadoHabitacion)
                .Where(x => x.EmpleadoId == idEmpleado)
                .Select(x => x.Habitacion)
                .Include(x => x.EstadoHabitacion);
                
        }
    }
}
