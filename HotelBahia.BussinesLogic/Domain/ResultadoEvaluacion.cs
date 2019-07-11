using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage = "La cantidad de tardanzas es requerida")]
        public int? Tardanzas { get; set; }
        [Required(ErrorMessage = "La cantidad de faltas es requerida")]
        public int? Faltas { get; set; }
        [Required(ErrorMessage = "La cantidad de malas supervisaciones es requerida")]
        public int? SupervisionesNegativas { get; set; }
        [Required(ErrorMessage = "La valoracion del supervisor es requerida")]
        public int? Valoracion { get; set; }
        public string Comentarios { get; set; }
        public string Evaluador { get; set; }

        public ICollection<EvaluacionSupervisor> EvaluacionSupervisor { get; set; }

    }
}
