using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

        public int LineId { get; set; }
        public BusLine Line { get; set; }
    }
}