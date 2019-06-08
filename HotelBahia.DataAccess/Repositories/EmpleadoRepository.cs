using HotelBahia.BussinesLogic.Contracts.Repositories;
using HotelBahia.BussinesLogic.Domain;
using HotelBahia.DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.DataAccess.Repositories
{
    public class EmpleadoRepository : Repository<Empleado>, IEmpleadoRepository
    {
        public EmpleadoRepository(HoteleriaContext context): base (context)
        {
            
        }
    }
}
