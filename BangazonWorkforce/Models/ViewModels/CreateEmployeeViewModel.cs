using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using BangazonWorkforce.Repositories;
using BangazonWorkforce.Models;

namespace StudentExercisesMVC.Models.ViewModels
{
    public class CreateEmployeeViewModel
    {
        public List<SelectListItem> Departments { get; set; }
        public Employee Employee { get; set; }
      



        public CreateEmployeeViewModel()
        {


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