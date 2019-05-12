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
        }
        
        public DtoB CheckOut(int nroHabitacion)
        {
            DtoB rsp = new DtoB();
            Habitacion hab = _habitacionRepository.BuscarPorNro(nroHabitacion);
            string estado;
            if(hab != null)
            {
               estado = hab.EstadoHabitacion.EstadoNombre;
               if(estado == "Ocupado")
                {
                    _habitacionRepository.EditarEstado(hab, "Desocupado");
                }
                else
                {
                    rsp.IsOk = false;
                }
            }
            else
            {
                rsp.IsOk = false;
            }
            if (rsp.IsOk)
            {
                //Aqui va el metodo de notificacion
            }
            return rsp;
        }
    }
}
