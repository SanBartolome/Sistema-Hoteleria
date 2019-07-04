using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Domain
{
    public partial class ResultadoEvaluacion
    {

        public ResultadoEvaluacion()
        {
            EvaluacionSupervisor = new HashSet<EvaluacionSupervisor>();
        }

        public int ResultadoEvaluacionId { get; set; }
        public int? Tardanzas { get; set; }
        public int? Faltas { get; set; }
        public int? SupervisionesNegativas { get; set; }
        public int? Valoracion { get; set; }
        public string Comentarios { get; set; }

        public ICollection<EvaluacionSupervisor> EvaluacionSupervisor { get; set; }

    }
}
