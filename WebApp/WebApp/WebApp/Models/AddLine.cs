using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class AddLine
    {
        public int Id { get; set; }
        public int SerialNumber { get; set; }
        public List<string> StationsAdd { get; set; }
    }
}