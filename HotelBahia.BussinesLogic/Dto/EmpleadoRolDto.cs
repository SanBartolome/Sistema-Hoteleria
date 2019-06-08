using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Dto
{
    public class EmpleadoRolDto
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string UsuarioNombre { get; set; }
        public int EmpleadoId { get; set; }
        public int RolId { get; set; }
        public string RolName { get; set; }
    }
}
