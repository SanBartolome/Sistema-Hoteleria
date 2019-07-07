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
            Incidencia = new HashSet<Incidencia>();
            ObjetoPerdido = new HashSet<ObjetoPerdido>();
            EvaluacionSupervisor = new HashSet<EvaluacionSupervisor>();
        }

        public int EmpleadoId { get; set; }
        [Required(ErrorMessage = "El nombre del empleado es requerido")]
        public string Nombres { get; set; }
        [Required( ErrorMessage = "El apellido del empleado es requerido" )]
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
        public int? Telefono { get; set; }
        [Required(ErrorMessage = "El correo del empleado es requerido")]
        [EmailAddress(ErrorMessage = "El correo ingresado es inválido")]
        public string Correo { get; set; }
        public string Sexo { get; set; }
        [Required(ErrorMessage = "El nombre de usuario es requerido")]
        public string UsuarioNombre { get; set; }
        public ICollection<AsignacionHabitacion> AsignacionHabitacion { get; set; }
        public ICollection<Incidencia> Incidencia { get; set; }
        public ICollection<ObjetoPerdido> ObjetoPerdido { get; set; }
        public ICollection<EvaluacionSupervisor> EvaluacionSupervisor { get; set; }
    }
}
