using HotelBahia.BussinesLogic.Dto;
using HotelBahia.DataAccess.Models;
using HotelBahia.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBahia.BussinesLogic.Servicios
{
    public class HabitacionService
    {
        private HabitacionRepository _habitacionRepository;

        public HabitacionService(HabitacionRepository habitacionRepository)
        {
            _habitacionRepository = habitacionRepository;
        }
        
        public DtoB CambiarEstado(int nroHabitacion, string nuevoEstado)
        {
            Dictionary<string, List<string>> restricciones = new Dictionary<string, List<string>>
            {
                { "Ocupado", new List<string>() { "Disponible" } },
                { "Desocupado", new List<string>() { "Ocupado" } },
                { "En Limpieza", new List<string>() { "Desocupado", "Limpieza Imcompleta" } },
                { "Limpieza Realizada", new List<string>() { "En Limpieza" } },
                { "Limpieza Incompleta", new List<string>() { "Limpieza Realizada" } },
                { "Habilitado", new List<string>() { "Limpieza Realizada" } }
            };

            DtoB rsp = new DtoB();
            Habitacion hab = _habitacionRepository.BuscarPorNro(nroHabitacion);
            string estado;
            if (hab != null)
            {
                estado = hab.EstadoHabitacion.EstadoNombre;
                
                foreach (var item in restricciones)
                {
                    if (nuevoEstado == item.Key && item.Value.Contains(estado))
                    {
                        _habitacionRepository.EditarEstado(hab, nuevoEstado);
                        _habitacionRepository.UnitOfWork.SaveChanges();
                        rsp.IsOk = true;
                        return rsp;
                    }
                }
                rsp.IsOk = false;
            }
            else
            {
                rsp.IsOk = false;
            }
            return rsp;
        }

        public Habitacion ObtenerPorId(int idHabitacion)
        {
            return _habitacionRepository.Find(x => x.HabitacionId == idHabitacion).FirstOrDefault();
        }
    }
}
