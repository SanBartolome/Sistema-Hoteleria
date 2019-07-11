using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class ObjetoPerdido
    {

        public int ObjetoPerdidoId { get; set; }
        public int EmpleadoId { get; set; }
        [Required(ErrorMessage = "El nombre del objeto perdido es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "La descripcion del objeto perdido es requerida")]
        public string Descripcion { get; set; }
        [Required(ErrorMessage = "La habitacion donde se encontro el objeto perdido es requerida")]
        public int? Habitacion { get; set; }
        public DateTime Fecha { get; set; }
        public int? Estado { get; set; }

        public Empleado Empleado { get; set; }

    }
}
