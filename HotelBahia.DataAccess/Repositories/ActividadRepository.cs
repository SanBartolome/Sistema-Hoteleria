using HotelBahia.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HotelBahia.DataAccess.Repositories
{
    public class ActividadRepository : Repository<Actividad>
    {
        public ActividadRepository(HoteleriaContext context): base(context)  
        {

        }

        
    }
}
