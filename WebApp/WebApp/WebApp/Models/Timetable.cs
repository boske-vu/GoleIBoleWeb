using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Timetable
    {
        public int Id { get; set; }

        public int TimetableTypeId { get; set; }
        public TimetableType TimetableType { get; set; }

        public int DayTypeId { get; set; }
        public DayType DayType { get; set; }
    }
}