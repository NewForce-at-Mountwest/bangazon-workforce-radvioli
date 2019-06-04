using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class Department
    {
        public int id { get; set; }

        [Display(Name = "Department Name")]
        public string name { get; set; }
        public int budget { get; set; }
        public List<Employee> DepartmentEmployees { get; set; }
    }
}
