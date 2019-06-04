using BangazonWorkforce.Models;
using BangazonWorkforce.Repositories;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class DepartmentEmployeesViewModel
    {
        public List<Employee> DepartmentEmployees { get; set; }

        public Department department { get; set; }

        public DepartmentEmployeesViewModel(int id)
        {
            
            department = DepartmentRepository.GetOneDepartment(id);

            //Need to start a List of Employees. Then Select the employees that have the Matching DepartmentId and add them to the List.
            //List<Employee> DepartmentEmployees = EmployeeRepository.GetAllEmployees();
            //    {

            //    }
                
            //    (e.DepartmentId = id);

        }
  
    }
}
