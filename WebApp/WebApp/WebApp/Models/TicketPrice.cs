using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    
    public class TicketPrice
    {
        public int Id { get; set; }
        [Required]
        public double Price { get; set; }

        public int PricelistId { get; set; }
        public PriceList Pricelist { get; set; }

        public int TicketTypeId { get; set; }
        public TicketType TicketType { get; set; }
    }
}