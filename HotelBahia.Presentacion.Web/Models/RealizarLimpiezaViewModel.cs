using HotelBahia.DataAccess.Models;
using HotelBahia.Presentacion.Web.Models.Estados;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models
{
    public class RealizarLimpiezaViewModel
    {
        public Habitacion habitacion { get; set; }
        public string Usuario { get; set; }
    }
}
