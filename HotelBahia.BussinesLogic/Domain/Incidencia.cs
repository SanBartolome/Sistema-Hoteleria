using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class Incidencia
    {

        public int IncidenciaID { get; set; }
        public int EmpleadoId { get; set; }
        [Required(ErrorMessage = "La habitacion donde se encuentra la incidencia es requerida")]
        public int? Habitacion { get; set; }
        public string Prioridad { get; set; }
        [Required(ErrorMessage = "La descripcion de la incidencia es requerida")]
        public string Descripcion { get; set; }
        public string Encargado { get; set; }
        public int? Estado { get; set; }
        public DateTime? FechaAbierto { get; set; }
        public DateTime? FechaCerrado { get; set; }

        public Empleado Empleado { get; set; }

    }
}
