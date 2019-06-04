using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace BangazonWorkforce.Models
{
    public class Computer
    {
        public int id { get; set;}
        public DateTime PurchaseDate { get; set;}
        public DateTime DecomissionDate { get; set;}
        [Display(Name = "Computer Make")]
        public string make { get; set;}
        [Display(Name = "Computer Manufacturer")]
        public string manufacturer { get; set;}

    }
}
