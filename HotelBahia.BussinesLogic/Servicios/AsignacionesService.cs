using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBahia.BussinesLogic.Servicios
{
    public class AsignacionesService
    {
        private IAsignacionesRepository _asignacionesRepository;
        public AsignacionesService(IAsignacionesRepository asignacionesRepository)
        {
            _asignacionesRepository = asignacionesRepository;
        }

        public Empleado EmpleadoAsignadoPorRol(int idHabitacion, int idRol)
        {
            try
            {
                return _asignacionesRepository.EmpleadoAsignadoPorRol(idHabitacion, idRol).Empleado;
            }
            catch (Exception)
            {

                return null;
            }
            
        }

        public List<Habitacion> HabitacionesAsignadas(int idEmpleado)
        {
            try
            {
                return _asignacionesRepository
                    .HabitacionesAsignadas(idEmpleado)
                    .Select(x => x.Habitacion)
                    .ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
