using AutoMapper;
using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Servicios
{
    public class ControlService
    {
        private readonly IHabitacionRepository _habitacionRepository;

        public ControlService(IHabitacionRepository habitacionRepository)
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
