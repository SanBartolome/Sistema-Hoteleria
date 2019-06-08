using System;
using System.Collections.Generic;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class Empleado
    {
        public Empleado()
        {
            AsignacionHabitacion = new HashSet<AsignacionHabitacion>();
        }

        public int EmpleadoId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public int? Telefono { get; set; }
        public string Correo { get; set; }
        public string Sexo { get; set; }
        public string UsuarioNombre { get; set; }
        public ICollection<AsignacionHabitacion> AsignacionHabitacion { get; set; }
    }
}
