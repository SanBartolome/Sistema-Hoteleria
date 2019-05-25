using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.BussinesLogic.Dto.Actividad
{
    public class ActividadDto : DtoB
    {
        public int ActividadId { get; set; }

        public int TipoActividadId { get; set; }

        public string Descripcion { get; set; }

        public int Estado { get; set; }
    }
}
