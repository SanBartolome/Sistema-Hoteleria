using HotelBahia.BussinesLogic.Domain.Enums;
using HotelBahia.DataAccess.Repositories;
using HotelBahia.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace HotelBahia.BussinesLogic.Servicios
{
    public class HabitacionService
    {
        private HabitacionRepository _habitacionRepository;

        public HabitacionService(HabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }

        public bool CheckOut(Habitacion habitacion)
        {
            try
            {
                if (habitacion.EstadoHabitacionId != (int)HabitacionEstado.Ocupado) return false;
                habitacion.EstadoHabitacionId = (int)HabitacionEstado.Desocupado;
                _habitacionRepository.Edit(habitacion);
                _habitacionRepository.UnitOfWork.SaveChanges();
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
