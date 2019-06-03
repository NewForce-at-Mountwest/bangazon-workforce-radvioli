using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BangazonWorkforce.Models;

namespace BangazonWorkforce.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IConfiguration _config;

        public EmployeeController(IConfiguration config)
        {
            _config = config;
        }

        public SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Employee
        public ActionResult Index()
        {
            {
                using (SqlConnection conn = Connection)
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {

                        //joins employee, department
                        string command = $@"SELECT e.Id AS 'Employee Id', e.FirstName, e.LastName, e.DepartmentId,
                        d.Id AS 'Department Id', d.Name AS 'Department'
                        FROM Employee e FULL JOIN Department d ON e.DepartmentId = d.Id";

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        List<Employee> employees = new List<Employee>();

                        while (reader.Read())
                        {


                            Employee employee = new Employee
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Employee Id")),
                                FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                LastName = reader.GetString(reader.GetOrdinal("LastName")),
                                CurrentDepartment = new Department()
                                {
                                    Name = reader.GetString(reader.GetOrdinal("Department")),

                                }
                            };






                            employees.Add(employee);
                        }
                        reader.Close();
                        return View(employees);
                    }
                }
            }
        }
    }
}