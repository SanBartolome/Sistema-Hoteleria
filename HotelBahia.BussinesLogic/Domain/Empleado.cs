using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class Empleado
    {
        public Empleado()
        {
            AsignacionHabitacion = new HashSet<AsignacionHabitacion>();
        }

        public int EmpleadoId { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public int? Telefono { get; set; }
        [Required]
        public string Correo { get; set; }
        public string Sexo { get; set; }
        [Required]
        public string UsuarioNombre { get; set; }
        public ICollection<AsignacionHabitacion> AsignacionHabitacion { get; set; }
    }
}
