﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models
{
    public class HabitacionesModel
    {
        public int idHabitacion { get; set; }

        public int NumeroHabitacion { get; set; }

        public int IdEstado { get; set; }

        public string Estado { get; set; }

        public string TipoHabitacion { get; set; }
    }
}