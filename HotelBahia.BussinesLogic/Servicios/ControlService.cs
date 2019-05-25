using AutoMapper;
using HotelBahia.BussinesLogic.Dto.Actividad;
using HotelBahia.DataAccess.Models;
using HotelBahia.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Servicios
{
    public class ControlService
    {
        private readonly HabitacionRepository _habitacionRepository;

        public ControlService(HabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }

        public List<Actividad> ObtenerActividadesDeHabPorEmpleado(int idHabitacion, int idEmpleado)
        {
            var result = _habitacionRepository.ObtenerActividadesPorEmpleado(idHabitacion, idEmpleado);
            return new List<Actividad>(result);
        }
    }
}
