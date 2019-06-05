
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using BangazonWorkforce;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using BangazonWorkforce.Repositories;

namespace BangazonWorkforce.Models.ViewModels
{
    public class DepartmentEmployeeListViewModel
    {
        internal Department department;

        [Display(Name = "Employee Count")]
        public int DepartmentSize { get; set; }

        public Department ThisDepartment { get; set; }

    }
}
