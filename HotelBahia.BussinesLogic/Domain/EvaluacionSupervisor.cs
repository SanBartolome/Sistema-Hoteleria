using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class EvaluacionSupervisor
    {

        public int EvaluacionSupervisorId { get; set; }
        public int? EmpleadoId { get; set; }
        public int? ResultadoEvaluacionId { get; set; }

        public Empleado Empleado { get; set; }
        public ResultadoEvaluacion ResultadoEvaluacion { get; set; }

    }
}
