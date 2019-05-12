using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models
{
    public class ActividadModel
    {
        public int ActividadId { get; set; }
        public string TipoActividadId { get; set; }
        public string Descripcion { get; set; }
        public int Estado { get; set; }

    }
}
