using HotelBahia.DataAccess.Models;
using HotelBahia.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Servicios
{
    public class AsignacionesService
    {
        private AsignacionesRepository _asignacionesRepository;
        public AsignacionesService(AsignacionesRepository asignacionesRepository)
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
    }
}
