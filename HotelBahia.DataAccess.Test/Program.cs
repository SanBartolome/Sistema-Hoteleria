using HotelBahia.BussinesLogic.Servicios;
using HotelBahia.DataAccess.Models;
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
            var a = repo.BuscarPorNro(11);
            Console.WriteLine("Hello World!");
        }
    }
}
