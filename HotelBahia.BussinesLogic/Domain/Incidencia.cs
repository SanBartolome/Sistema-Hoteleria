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
        public int? Habitacion { get; set; }
        public string Prioridad { get; set; }
        public string Descripcion { get; set; }
        public string Encargado { get; set; }
        public int? Estado { get; set; }
        public DateTime? FechaAbierto { get; set; }
        public DateTime? FechaCerrado { get; set; }

        public Empleado Empleado { get; set; }

    }
}
