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
        public ICollection<Incidencia> Incidencia { get; set; }
        public ICollection<ObjetoPerdido> ObjetoPerdido { get; set; }
        public ICollection<EvaluacionSupervisor> EvaluacionSupervisor { get; set; }
    }
}
