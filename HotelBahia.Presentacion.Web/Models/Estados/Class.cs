using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.Presentacion.Web.Models.Estados
{
    public enum HabitacionEstado
    {
        Disponible = 1,
        Ocupado = 2,
        Desocupado = 3,
        EnLimpieza = 4,
        LimpiezaRealizada = 5,
        LimpiezaIncompleta = 6,
    }

    public enum LimpiezaEstado
    {
        Asignada = 0,
        Iniciada = 1,
        Terminada = 2,
    }
}
