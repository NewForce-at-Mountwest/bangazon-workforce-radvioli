﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class PaymentType
    {
        public int id { get; set; }
        public string name { get; set; }
        public int accountNumber { get; set; }
        public int CustomerId { get; set; }
        
       
    }
}
