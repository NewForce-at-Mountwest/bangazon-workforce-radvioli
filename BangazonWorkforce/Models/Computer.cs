﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BangazonAPI.Models
{
    public class Computer
    {
        public int id { get; set;}
        public DateTime PurchaseDate { get; set;}
        public DateTime DecomissionDate { get; set;}
        public string make { get; set;}
        public string manufacturer { get; set;}

    }
}
