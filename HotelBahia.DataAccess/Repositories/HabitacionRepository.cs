using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace HotelBahia.DataAccess.Repositories
{
    public class HabitacionRepository : Repository<Habitacion>, IHabitacionRepository
    {
        public HabitacionRepository(HoteleriaContext context) : base(context)
        {

        }

        public IEnumerable<Habitacion> GetAllComplete()
        {
            return _context.Habitacion
               .Include(x => x.EstadoHabitacion)
               .Include(x => x.TipoHabitacion);
        } 

        public Habitacion BuscarPorNro(int numero)
        {
            return _context.Habitacion
                .Include(x => x.EstadoHabitacion)
                .SingleOrDefault(x => x.Numero == numero);
        }

        public void EditarEstado(Habitacion habitacion, string estadoNombre)
        {
            var estado = _context.EstadoHabitacion.Where(x => x.EstadoNombre == estadoNombre).FirstOrDefault();
            habitacion.EstadoHabitacion = estado;
            Edit(habitacion);
        }

        public IEnumerable<Actividad> ObtenerActividades(int idHabitacion)
        {
            var hab = _context.Habitacion
                    .Single(x => x.HabitacionId == idHabitacion);

            return _context.Entry(hab)
             .Collection(x => x.HabitacionActividad)
             .Query()
             .Select(x => x.Actividad);
        }
        public IEnumerable<Actividad> ObtenerActividades(int idHabitacion, int tipoActividadId)
        {
            var hab = _context.Habitacion
                    .Single(x => x.HabitacionId == idHabitacion);

            return _context.Entry(hab)
             .Collection(x => x.HabitacionActividad)
             .Query()
             .Where(x => x.Actividad.TipoActividadId == tipoActividadId)
             .Select(x => x.Actividad);
        }

        public Habitacion ObtenerConActividades(int idHabitacion)
        {
            return _context.Habitacion
                    .Include(x => x.HabitacionActividad)
                    .ThenInclude(x => x.Actividad)
                    .Single(x => x.HabitacionId == idHabitacion);
        }

        public Habitacion ObtenerConIncidencias(int idHabitacion)
        {
            return _context.Habitacion
                    .Include(x => x.AsignacionHabitacion)
                    .ThenInclude(x => x.Empleado)
                    .ThenInclude(x => x.Incidencia)
                    .Single(x => x.HabitacionId == idHabitacion);
        }

        public Habitacion ObtenerConActividades(int idHabitacion, int tipoActividadId)
        {
            return _context.Habitacion
                    .Include(x => x.HabitacionActividad)
                    .ThenInclude(x => x.Actividad)
                    .Single(x => x.HabitacionId == idHabitacion);
        }

        public IEnumerable<Actividad> ObtenerActividadesPorEmpleado(int idHabitacion, int idEmpleado)
        {
            SqlParameter[] parametros = new SqlParameter[]
            {   new SqlParameter("@IdEmpleado", idEmpleado),
                new SqlParameter("@IdHabitacion", idHabitacion)
            };
            var result = _context.Actividad
                 .FromSql("HabitacionObtenerActividadesPorEmpleadoSP @IdEmpleado, @IdHabitacion", parametros);
            return result.ToList();
        }
    }
}
