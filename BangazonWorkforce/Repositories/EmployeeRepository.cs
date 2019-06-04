﻿using System;
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

        public static Employee GetOneEmployee(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT e.Id AS 'Employee Id', e.FirstName, e.LastName, e.IsSuperVisor, e.DepartmentId,
                        d.Id AS 'Department Id', d.Name AS 'Department', d.Budget ,c.Id AS 'Computer Id', tp.Name, tp.startDate, tp.endDate,
						c.Make, c.Manufacturer, c.PurchaseDate, c.DecomissionDate, tp.Id AS 'Training Id'
                        FROM Employee e JOIN Department d ON e.DepartmentId = d.Id
						LEFT JOIN ComputerEmployee ce ON e.Id = ce.EmployeeId
                        LEFT JOIN Computer c ON ce.ComputerId=c.Id JOIN EmployeeTraining et ON e.Id = et.EmployeeId LEFT JOIN TrainingProgram tp ON et.TrainingProgramId = tp.Id WHERE e.Id = @id ";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Employee employee = null;

                    while (reader.Read())
                    {

                        if (employee == null)
                        {
                            employee = new Employee
                            {
                                id = reader.GetInt32(reader.GetOrdinal("Employee Id")),
                                firstName = reader.GetString(reader.GetOrdinal("FirstName")),
                                lastName = reader.GetString(reader.GetOrdinal("LastName")),
                                DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                                employeesDepartment = new Department()
                                {
                                    id = reader.GetInt32(reader.GetOrdinal("Department Id")),
                                    name = reader.GetString(reader.GetOrdinal("Department")),
                                },

                                employeeComputer = null,
                                TrainingPrograms = new List<TrainingProgram>()


                            };
                        }

                        TrainingProgram trainingProgram = new TrainingProgram()
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Training Id")),
                            name = reader.GetString(reader.GetOrdinal("Name")),
                        };

                        employee.TrainingPrograms.Add(trainingProgram);

                        if (!reader.IsDBNull(reader.GetOrdinal("Computer Id")))
                        {
                            Computer computer = new Computer()
                            {
                                make = reader.GetString(reader.GetOrdinal("Make")),
                                manufacturer = reader.GetString(reader.GetOrdinal("Manufacturer"))
                            };
                            employee.employeeComputer = computer;
                        }
                    }
                    reader.Close();
                    return employee;
                }
            }
        }
    }
}