using HotelBahia.BussinesLogic.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models
{
    public class EvaluacionModel
    {
        public ResultadoEvaluacion ResultadoEvaluacion { get; set; }
        public EvaluacionSupervisor EvaluacionSupervisor { get; set; }
        public Empleado Empleado { get; set; }
        public List<ResultadoEvaluacion> ResultadosEvaluaciones { get; set; }
        public List<EvaluacionSupervisor> EvaluacionesSupervisores { get; set; }
        public List<Empleado> Empleados { get; set; }
        public string Usuario { get; set; }
    }
}
