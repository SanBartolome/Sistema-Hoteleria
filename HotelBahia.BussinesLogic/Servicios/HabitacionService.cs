using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.DataAccess.Repositories;
using HotelBahia.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using HotelBahia.BussinesLogic.Servicios.AppServices;

namespace HotelBahia.BussinesLogic.Servicios
{
    public class HabitacionService
    {
        private HabitacionRepository _habitacionRepository;
        private AsignacionesRepository _asignacionesRepository;

        public HabitacionService(HabitacionRepository habitacionRepository, AsignacionesRepository asignacionesRepository)
        {
            _habitacionRepository = habitacionRepository;
            _asignacionesRepository = asignacionesRepository;
        }

        public bool CheckOut(Habitacion habitacion)
        {
            try
            {
                if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.Ocupado) return false;
                habitacion.EstadoHabitacionId = (int)HabitacionEstado.Desocupado;
                _habitacionRepository.Edit(habitacion);
                _habitacionRepository.UnitOfWork.SaveChanges();
                var empleado = new AsignacionesService(_asignacionesRepository).EmpleadoAsignadoPorRol(habitacion.HabitacionId, (int)RolEnum.AgenteDeLimpieza);
                new NotificacionService().Notificar(empleado, habitacion, ActividadTipo.Limpieza);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Actividad> ObtenerActividades(int idHabitacion)
        {
            try
            {
                return _habitacionRepository.ObtenerActividades(idHabitacion).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Habitacion ObtenerConActividades(int idHabitacion, ActividadTipo tipo)
        {
            try
            {
                return _habitacionRepository.ObtenerConActividades(idHabitacion, (int)tipo);
            }
            catch (Exception)
            {
                return null;
            }
        }

        

        public Habitacion Obtener(int id)
        {
            try
            {
                return _habitacionRepository.Find(x => x.HabitacionId == id).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public Habitacion BuscarPorNro(int nroHabitacion)
        {
            try
            {
                return _habitacionRepository.Find(x => x.Numero == nroHabitacion).FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Habitacion> Filtrar(Expression<Func<Habitacion, bool>> predicate)
        {
            try
            {
                return _habitacionRepository.Find(predicate).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
