using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BangazonWorkforce.Models;
using BangazonWorkforce.Repositories;

namespace BangazonWorkforce.Controllers
{
    public class EmployeeController : Controller
    {

    

        public EmployeeController(IConfiguration config)
        {
            EmployeeRepository.SetConfig(config);
        }

        // GET: Employees

        public ActionResult Index()
        {
            List<Employee> students = EmployeeRepository.GetEmployees();
            return View(students);

        }

    }
}