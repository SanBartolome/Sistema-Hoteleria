using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class EvaluacionSupervisor
    {

        public int EvaluacionSupervisorId { get; set; }
        [Required(ErrorMessage = "El supervisor es requerido")]
        public int? EmpleadoId { get; set; }
        [Required(ErrorMessage = "La evaluacion es requerida")]
        public int? ResultadoEvaluacionId { get; set; }

        public Empleado Empleado { get; set; }
        public ResultadoEvaluacion ResultadoEvaluacion { get; set; }

    }
}
