using BangazonWorkforce.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class EmployeeEditViewModel
    {
        [Display(Name = "Departments")]
        public List<SelectListItem> Departments { get; set; } = new List<SelectListItem>();
        public List<int> SelectedDepartment { get; set; }
        public List<SelectListItem> Computers { get; set; }

        public Employee Employee { get; set; }

        public EmployeeEditViewModel() { }

        public EmployeeEditViewModel(int employeeId)
        {
            Employee = EmployeeRepository.GetOneEmployee(employeeId);
            //employee.employeesDepartment = DepartmentRepository.GetOneDepartment(employeeId);

            Departments = DepartmentRepository.GetDepartments()
                .Select(department => new SelectListItem()
                {
                    Text = department.ThisDepartment.name,
                    Value = department.ThisDepartment.id.ToString()

                })
                .ToList();

            Departments.Insert(0, new SelectListItem
            {
                Text = "Choose a Department",
                Value = "0"
            });

            
        }
    }
}
