﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonAPI.Models
{
    public class Customer
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public DateTime accountCreated { get; set; }
        public DateTime lastActive { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();

        public List<PaymentType> PaymentTypes { get; set; } = new List<PaymentType>();
    }
}
