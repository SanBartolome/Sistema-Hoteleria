using HotelBahia.BussinesLogic.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models
{
    public class EvaluacionModel
    {
        [Required(ErrorMessage = "La evaluacion es requerida")]
        public ResultadoEvaluacion ResultadoEvaluacion { get; set; }
        public EvaluacionSupervisor EvaluacionSupervisor { get; set; }
        public Empleado Empleado { get; set; }
        public string Usuario { get; set; }
    }
}
