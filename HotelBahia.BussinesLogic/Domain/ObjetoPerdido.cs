using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class ObjetoPerdido
    {

        public int ObjetoPerdidoId { get; set; }
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? Habitacion { get; set; }
        public DateTime Fecha { get; set; }
        public int? Estado { get; set; }

        public Empleado Empleado { get; set; }

    }
}
