using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class RedVoznjeInfoBindingModel
    {
        public List<TimetableType> TimetableTypes { get; set; }
        public List<BusLine> BusLines { get; set; }
        public List<DayType> DayTypes { get; set; }

    }
}