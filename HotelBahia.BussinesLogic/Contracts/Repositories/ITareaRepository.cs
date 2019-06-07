﻿using HotelBahia.BussinesLogic.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Contracts.Repositories
{
    public interface ITareaRepository : IRepository<HabitacionActividad>
    {
        IEnumerable<HabitacionActividad> GetActividadesTarea(int idHabitacion);
    }
}
