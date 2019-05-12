using HotelBahia.BussinesLogic.Dto;
using HotelBahia.DataAccess.Models;
using HotelBahia.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Servicios
{
    public class HabitacionService
    {
        private HabitacionRepository _habitacionRepository;

        public HabitacionService(HabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
            CheckOut(1);
        }
        
        public DtoB CheckOut(int nroHabitacion)
        {
            DtoB rsp = new DtoB();
            Habitacion h = _habitacionRepository.BuscarPorNro(nroHabitacion);
            if(h != null)
            {
               
            }


            return rsp;
        }
    }
}
