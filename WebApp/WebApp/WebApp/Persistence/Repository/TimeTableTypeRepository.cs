using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using WebApp.Models;

namespace WebApp.Persistence.Repository
{
    public class TimeTableTypeRepository : Repository<TimetableType, int>, ITimeTableTypeRepository
    {
        public TimeTableTypeRepository(DbContext context) : base(context)
        {

        }
    }
}