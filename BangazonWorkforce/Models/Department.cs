using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class Department
    {
        public int id { get; set; }
        public string name { get; set; }
        public int budget { get; set; }
        public List<Employee> DepartmentEmployees { get; set; }
    }
}