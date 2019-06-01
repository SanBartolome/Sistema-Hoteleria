using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBahia.BussinesLogic.Domain.Enums
{
    public enum HabitacionEstado
    {
        Disponible = 1,
        Ocupado = 2,
        Desocupado = 3,
        EnLimpieza = 4,
        [Display(Name="Limpieza Realizada")]
        LimpiezaRealizada = 5,
        LimpiezaIncompleta = 6,
    }

    public enum LimpiezaEstado
    {
        Asignada = 0,
        Iniciada = 1,
        Terminada = 2,
    }

    public enum ActividadEstado
    {
        Mal = 3,
        Regular = 2,
        Bien = 1,
    }
}
