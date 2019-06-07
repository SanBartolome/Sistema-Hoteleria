using System.Collections.Generic;
using HotelBahia.BussinesLogic.Domain;

namespace HotelBahia.BussinesLogic.Contracts.Repositories
{
    public interface IAsignacionesRepository
    {
        AsignacionHabitacion EmpleadoAsignadoPorRol(int idHabitacion, int idRol);
        IEnumerable<Habitacion> HabitacionesAsignadas(int idEmpleado);
    }
}