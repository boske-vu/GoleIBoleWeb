using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class TimetableType
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public List<Timetable> Timetables { get; set; }
    }
}