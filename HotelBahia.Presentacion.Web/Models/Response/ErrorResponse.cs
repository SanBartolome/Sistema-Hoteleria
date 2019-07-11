using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models.Response
{
    public class ErrorResponse
    {

        public string errorCode { get; set; }
        public string[] messages { get; set; }

    }
}
