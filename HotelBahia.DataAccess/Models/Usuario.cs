using System;
using System.Collections.Generic;

namespace HotelBahia.DataAccess.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Empleado = new HashSet<Empleado>();
        }

        public string UsuarioNombre { get; set; }
        public string Password { get; set; }
        public int? RolId { get; set; }

        public Rol Rol { get; set; }
        public ICollection<Empleado> Empleado { get; set; }
    }
}
