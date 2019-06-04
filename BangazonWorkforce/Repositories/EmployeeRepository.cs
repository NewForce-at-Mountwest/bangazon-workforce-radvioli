using BangazonWorkforce.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.EmployeeRepository
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

        public static List<Employee> GetAllEmployees()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT
                            e.Id, e.firstName, e.lastName, e.DepartmentId, e.ComputerId
       
                        FROM Employee e
                        JOIN Department d ON e.DepartmentId = d.Id
                        JOIN Computer c ON e.ComputerId = c.Id";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Employee> employees = new List<Employee>();
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            firstName = reader.GetString(reader.GetOrdinal("FirstName")),
                            lastName = reader.GetString(reader.GetOrdinal("LastName")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            ComputerId = reader.GetInt32(reader.GetOrdinal("ComputerId")),
                            employeeComputer = new Computer
                            {
                                make = reader.GetString(reader.GetOrdinal("Computer Make")),
                                manufacturer = reader.GetString(reader.GetOrdinal("Computer Manufacturer"))
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
                    cmd.CommandText = @"
                        SELECT
                            e.Id, e.firstName, e.lastName, e.DepartmentId, e.ComputerId
                        FROM Employee e
                        JOIN Department d ON e.DepartmentId = d.Id
                        JOIN Computer c ON e.ComputerId = c.Id
                        WHERE e.Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Employee Employee = null;

                    if (reader.Read())
                    {
                        Employee = new Employee
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            firstName = reader.GetString(reader.GetOrdinal("firstName")),
                            lastName = reader.GetString(reader.GetOrdinal("lastName")),
                            DepartmentId = reader.GetInt32(reader.GetOrdinal("DepartmentId")),
                            ComputerId = reader.GetInt32(reader.GetOrdinal("ComputerId")),
                            employeeComputer = new Computer
                            {
                                make = reader.GetString(reader.GetOrdinal("Computer Make")),
                                manufacturer = reader.GetString(reader.GetOrdinal("Computer Manufacturer"))
                            },
                            employeeDepartment = new Department
                            {
                                name = reader.GetString(reader.GetOrdinal("Department Name")),
                                budget = reader.GetInt32(reader.GetOrdinal("Department Budget"))
                            }
                        };
                    }
                    reader.Close();

                    return Employee;
                }
            }

        }

        public static Employee GetOneEmployeeWithDepartment(int id)
        {
            Employee employee = GetOneEmployeeWithDepartment(id);
            employee.employeeDepartment = DepartmentRepository.GetDepartmentByEmployee(id);
            return employee;
        }

        public static void CreateEmployee()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Employee
                ( FirstName, LastName, DepartmentId, ComputerId )
                VALUES
                ( @firstName, @lastName, @DepartmentId, @ComputerId )";
                    cmd.Parameters.Add(new SqlParameter("@firstName", firstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", lastName));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
                    cmd.Parameters.Add(new SqlParameter("@ComputerId", ComputerId));
                    cmd.ExecuteNonQuery();


                }
            }

        }

        public static void UpdateEmployee(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string command = @"UPDATE Employee
                                            SET firstName=@firstName, 
                                            lastName=@lastName, 
                                            DepartmentId=@DepartmentId, 
                                            ComputerId=@ComputerId
                                            WHERE Id = @id";


                    cmd.CommandText = command;
                    cmd.Parameters.Add(new SqlParameter("@firstName", firstName));
                    cmd.Parameters.Add(new SqlParameter("@lastName", lastName));
                    cmd.Parameters.Add(new SqlParameter("@DepartmentId", DepartmentId));
                    cmd.Parameters.Add(new SqlParameter("@ComputerId", ComputerId));
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int rowsAffected = cmd.ExecuteNonQuery();

                }

            }

        }
    }
}