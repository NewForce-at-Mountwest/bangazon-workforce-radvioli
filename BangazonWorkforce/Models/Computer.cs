using System;
using System.Linq;
using System.Threading.Tasks;


namespace BangazonWorkforce.Models
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
