
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class Employee
    {
        public int id { get; set; }
        [Display(Name = "First Name")]
        public string firstName { get; set; }
        [Display(Name = "Last Name")]
        public string lastName { get; set; }
        public bool isSupervisor { get; set; }
        public int DepartmentId { get; set; }
        public int ComputerId { get; set; }

        [Display(Name = "Current Department")]


        public Department employeesDepartment { get; set; } = new Department();

        public Computer employeeComputer { get; set; } = new Computer();
    }
}