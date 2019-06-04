using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using BangazonWorkforce.Models;


namespace BangazonWorkforce.Repositories
{
    public class EmployeeRepository
    {

        private static IConfiguration _config;

        public static void SetConfig(IConfiguration configuration)
        {
            _config = configuration;
        }


        public static SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        // GET: Students
        public static List<Employee> GetEmployees()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                     SELECT e.Id,
                     e.firstName,
                     e.lastName,
                     d.[Name]
                     FROM Employee e FULL JOIN Department d ON e.DepartmentId = d.Id";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Employee> employees = new List<Employee>();
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            firstName = reader.GetString(reader.GetOrdinal("firstName")),
                            lastName = reader.GetString(reader.GetOrdinal("lastName")),
                            employeesDepartment = new Department()
                            {
                                name = reader.GetString(reader.GetOrdinal("name")),

                            }

                        };

                        employees.Add(employee);
                    }

                    reader.Close();

                    return employees;
                }
            }
        }
    }
}