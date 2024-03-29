﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class Habitacion
    {
        public Habitacion()
        {
            AsignacionHabitacion = new HashSet<AsignacionHabitacion>();
            HabitacionActividad = new HashSet<HabitacionActividad>();
        }
        
        public int HabitacionId { get; set; }
        [Required(ErrorMessage = "El numero de la habitacion es requerido")]
        public int? Numero { get; set; }
        public int? Piso { get; set; }
        public int? EstadoHabitacionId { get; set; }
        public int? TipoHabitacionId { get; set; }
        public bool IsDelete { get; set; }

        public EstadoHabitacion EstadoHabitacion { get; set; }
        public TipoHabitacion TipoHabitacion { get; set; }

        [JsonIgnore]
        public ICollection<AsignacionHabitacion> AsignacionHabitacion { get; set; }
        [JsonIgnore]
        public ICollection<HabitacionActividad> HabitacionActividad { get; set; }
    }
}
