using HotelBahia.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Servicios
{
    public class HabitacionService
    {
        private readonly HabitacionRepository _habitacionRepository;
        public HabitacionService()
        {
            _habitacionRepository = new HabitacionRepository();
        }

        public void CambiarEstado(int idHabitacion)
        {

        }
    }
}
