using HotelBahia.BussinesLogic.Servicios;
using HotelBahia.DataAccess.Context;
using HotelBahia.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HotelBahia.DataAccess.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            HoteleriaContext context = new HoteleriaContext();
            HabitacionRepository repo = new HabitacionRepository(context);
            //var a = repo.BuscarPorNro(11);
            ControlService serv = new ControlService(repo);

            var a = serv.ObtenerActividadesDeHabPorEmpleado(1, 3);
            var b = repo.ObtenerActividadesPorEmpleado(1, 3);



            Console.WriteLine("Hello World!");
        }
    }
}
