using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AddStation
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public List<string> LinesAdd { get; set; }
    }
}