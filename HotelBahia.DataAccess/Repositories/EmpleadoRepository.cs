using HotelBahia.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.DataAccess.Repositories
{
    public class EmpleadoRepository : Repository<Empleado>
    {
        public EmpleadoRepository(HoteleriaContext context) : base(context)
        {

        }
    }
}
