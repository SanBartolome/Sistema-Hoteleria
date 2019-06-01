using System.Collections.Generic;
using HotelBahia.BussinesLogic.Domain;

namespace HotelBahia.BussinesLogic.Contracts.Repositories
{
    public interface IHabitacionRepository: IRepository<Habitacion>
    {
        Habitacion BuscarPorNro(int numero);
        void EditarEstado(Habitacion habitacion, string estadoNombre);
        IEnumerable<Habitacion> GetAllComplete();
        IEnumerable<Actividad> ObtenerActividades(int idHabitacion);
        IEnumerable<Actividad> ObtenerActividades(int idHabitacion, int tipoActividadId);
        IEnumerable<Actividad> ObtenerActividadesPorEmpleado(int idHabitacion, int idEmpleado);
        Habitacion ObtenerConActividades(int idHabitacion, int tipoActividadId);
    }
}