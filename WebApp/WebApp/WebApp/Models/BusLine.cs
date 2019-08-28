using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class BusLine
    {
        public int Id { get; set; }

        [Required]
        public int SerialNumber { get; set; }

        public List<Vehicle> Vehicles { get; set; }
        public virtual ICollection<Station> Stations { get; set; }
        public List<Timetable> Timetables { get; set; }
    }
}