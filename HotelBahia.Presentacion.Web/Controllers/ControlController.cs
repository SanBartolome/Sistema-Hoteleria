using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HotelBahia.BussinesLogic.Dto;
using HotelBahia.BussinesLogic.Dto.Actividad;
using HotelBahia.BussinesLogic.Servicios;
using HotelBahia.Presentacion.Web.Models;
using HotelBahia.Presentacion.Web.Models.Estados;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HotelBahia.Presentacion.Web.Controllers
{
    public class ControlController : Controller
    {
        private readonly HabitacionService _habitacionService;
        private readonly ControlService _controlService;

        public ControlController(HabitacionService habitacionService, ControlService controlService)
        {
            _habitacionService = habitacionService;
            _controlService = controlService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckOut(int nroHabitacion)
        {
            DtoB dto = new DtoB();
            dto = _habitacionService.CambiarEstado(nroHabitacion, "Desocupado");
            if (dto.IsOk) {
                ViewData["Mensaje"] = "Correcto, La habitación paso a estado de Desocupado";
            }
            else
            {
                ViewData["Mensaje"] = "Error, La Habitacion solicitada NO puede cambiar a estado Desocupado";
            }
            return View();
        }

        [HttpGet]
        public IActionResult RealizarLimpieza(int id)
        {
            int idEmpleado = HttpContext.Session.GetInt32("IdEmpleado") ?? 3;
            HttpContext.Session.SetInt32("IdEmpleado", idEmpleado);
            int idHabitacion;
            RealizarLimpiezaViewModel model = new RealizarLimpiezaViewModel();
            if (HttpContext.Session.GetInt32("IdHabitacion") != null 
               && HttpContext.Session.GetInt32("IdHabitacion") == id
               && HttpContext.Session.GetString("RealizarLimpieza") != null) 
            {
                idHabitacion = id;
                model = JsonConvert.DeserializeObject<RealizarLimpiezaViewModel>(HttpContext.Session.GetString("RealizarLimpieza"));
            }
            else
            {
                var hab = _habitacionService.ObtenerPorId(id);
                model.Actividades = new List<Actividad>();
                model.EstadoHabitacion = (HabitacionEstado)hab.EstadoHabitacionId;
                model.NroHabitacion = (int)hab.Numero;
                model.IdHabitacion = hab.HabitacionId;
                foreach (var item in _controlService.ObtenerActividadesDeHabPorEmpleado(model.IdHabitacion, idEmpleado))
                {
                    model.Actividades.Add(new Actividad() { IdActividad = item.ActividadId, Descripcion = item.Descripcion });
                }
            }

            switch (model.EstadoLimpieza)
            {
                case LimpiezaEstado.Asignada:
                    @ViewData["AccionBoton"] = "Iniciar Limpieza";
                    break;
                case LimpiezaEstado.Iniciada:
                    @ViewData["AccionBoton"] = "Terminar Limpieza";
                    break;
                case LimpiezaEstado.Terminada:
                    @ViewData["AccionBoton"] = "Terminar Limpieza";
                    break;
                default:
                    break;
            }
            HttpContext.Session.SetString("RealizarLimpieza", JsonConvert.SerializeObject(model));
            HttpContext.Session.SetInt32("Idhabitacion", model.IdHabitacion);
            return View(model);
        }

        [HttpPost]
        public IActionResult RealizarLimpieza()
        {
            ////RealizarLimpiezaViewModel model = CargarRealizarLimpiezaModel(id, 3);
            //switch (model.EstadoLimpieza)
            //{
            //    case LimpiezaEstado.Asignada:
            //        @ViewData["AccionBoton"] = "Iniciar Limpieza";
            //        break;
            //    case LimpiezaEstado.Iniciada:
            //        @ViewData["AccionBoton"] = "Terminar Limpieza";
            //        break;
            //    case LimpiezaEstado.Terminada:
            //        @ViewData["AccionBoton"] = "Terminar Limpieza";
            //        break;
            //    default:
            //        break;
            //}
            return View(  );
        }
    }
}