using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class Usuario
    {
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string UsuarioNombre { get; set; }
        [Required(ErrorMessage = "La contraseña es requerida")]
        public string Password { get; set; }
        public int? RolId { get; set; }
        public Rol Rol { get; set; }
        public Empleado Empleado { get; set; }
    }
}
