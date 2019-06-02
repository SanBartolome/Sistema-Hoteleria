using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class Usuario
    {
        public string UsuarioNombre { get; set; }
        [Required]
        public string Password { get; set; }
        public int? RolId { get; set; }
        public Rol Rol { get; set; }
        public Empleado Empleado { get; set; }
    }
}
