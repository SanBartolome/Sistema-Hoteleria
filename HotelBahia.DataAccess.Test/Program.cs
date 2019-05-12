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
            Repository<Rol> r = new Repository<Rol>(new HoteleriaContext());
            var _context = new HoteleriaContext();
            var blogs = _context.FromSql("SELECT * FROM dbo.Rol")
    .ToList();
            var a =  r.Find(x => x.RolId == 1);
            Console.WriteLine("Hello World!");
        }
    }
}
