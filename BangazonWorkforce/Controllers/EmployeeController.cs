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
using BangazonWorkforce.Models.ViewModels;

namespace BangazonWorkforce.Controllers
{
    public class EmployeeController : Controller
    {
        public EmployeeController(IConfiguration config)
        {
            DepartmentRepository.SetConfig(config);
            EmployeeRepository.SetConfig(config);
        }


        // GET: Employees

        public ActionResult Index()
        {
            List<Employee> employees = EmployeeRepository.GetEmployees();
            return View(employees);

        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            Employee employee = EmployeeRepository.GetOneEmployee(id);
            return View(employee);
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            CreateEmployeeViewModel employeeViewModel = new CreateEmployeeViewModel();
            return View(employeeViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateEmployeeViewModel model)
        {
            try
            {
                EmployeeRepository.CreateEmployee(model);
                return RedirectToAction(nameof(Index));

            }
            catch
            {
                return View();
            }


        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

    }
}