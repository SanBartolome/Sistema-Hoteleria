using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Helpers
{
    [Serializable]
    public class Message
    {
        public MessageType Tipo { get; set; }
        public string Contenido { get; set; }
        public string Titulo { get; set; }
    }

    [Serializable]
    public enum MessageType
    {
        success,
        danger,
        warning,
        info
    }
}
