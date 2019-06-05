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

            EmployeeRepository.SetConfig(config);
            DepartmentRepository.SetConfig(config);
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
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

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
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel(id);

            return View(employeeEditViewModel);
        }

        // POST: Employee/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeEditViewModel employeeEditViewModel)
        {
            try
            {
                EmployeeRepository.EditEmployee(id, employeeEditViewModel);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                return View(employeeEditViewModel);
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