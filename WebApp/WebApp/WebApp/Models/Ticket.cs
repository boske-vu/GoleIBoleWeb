﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public double FinalPrice { get; set; }

        public int PricelistId { get; set; }
        public Pricelist Pricelist { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string TransactionId { get; set; }
    }
}