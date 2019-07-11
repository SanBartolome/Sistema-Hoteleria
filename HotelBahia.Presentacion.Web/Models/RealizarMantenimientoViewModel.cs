using HotelBahia.BussinesLogic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models
{
    public class RealizarMantenimientoViewModel
    {
        public Habitacion Habitacion { get; set; }
        public Incidencia Incidencia { get; set; }
        public string Usuario { get; set; }
    }
}
