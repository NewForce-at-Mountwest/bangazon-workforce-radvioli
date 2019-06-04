using BangazonWorkforce.Models;
using BangazonWorkforce.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Repositories
{
    public class DepartmentRepository
    {
        private static IConfiguration _config;
        public static void SetConfig(IConfiguration configuration)
        {
            _config = configuration;
        }
        public static SqlConnection connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }
        public static List<DepartmentEmployeeListViewModel> GetDepartments()
        {
            using (SqlConnection conn = connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT
                        d.Id,
                        d.Name AS 'Department Name',
                        d.Budget ,
                        COUNT(*) AS 'Department Size'
                        FROM Employee e
                        JOIN Department d
                        ON e.DepartmentId = d.Id
                        GROUP BY d.Id, d.Name, d.Budget";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<DepartmentEmployeeListViewModel> departments = new List<DepartmentEmployeeListViewModel>();
                    while (reader.Read())
                    {
                        // create new view model r=including the properties of department, and
                        DepartmentEmployeeListViewModel currentDepartment = new DepartmentEmployeeListViewModel
                        {
                            ThisDepartment = new Department
                            {
                                id = reader.GetInt32(reader.GetOrdinal("Id")),
                                name = reader.GetString(reader.GetOrdinal("Department Name")),
                                budget = reader.GetInt32(reader.GetOrdinal("Budget")),
                            },
                            DepartmentSize = reader.GetInt32(reader.GetOrdinal("Department Size")),
                        };
                        departments.Add(currentDepartment);
                    }
                    reader.Close();
                    return departments;
                }
            }
        }
        public static Department GetOneDepartment(int id)
        {
            using (SqlConnection conn = connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT d.Id,
                            d.Name AS 'Department Name',
                            d.Budget
                    FROM Department d";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();
                    Department department = null;
                    if (reader.Read())
                    {
                        department = new Department
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            name = reader.GetString(reader.GetOrdinal("Department Name")),
                            budget = reader.GetInt32(reader.GetOrdinal("Budget"))

                        };
                    }
                    reader.Close();

                    return department;

                }
            }
        }
        //TODO: need to build out withEmployees method here...

        //public static Department GetOneDepartmentWithEmployees(int id)
        //{
            //List<Department> departmentEmployees = new List<Department>();

            //using (SqlConnection conn = connection)
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = conn.CreateCommand())
            //    {
            //        cmd.CommandText = @"
            //        SELECT d.Id,
            //                d.Name AS 'Department Name',
            //                e.firstName AS 'Employee First Name',
            //                e.lastName AS 'Employee Last Name'
            //        FROM Department d
            //        JOIN Employee e ON e.departmentId = d.Id";
            //        cmd.Parameters.Add(new SqlParameter("@id", id));
            //        SqlDataReader reader = cmd.ExecuteReader();

            //        while (reader.Read())
            //        {
                        // This needs to get id and name from the department and first and last name from employee
                        //departmentEmployees.Add(new Department
                        //{
                        //    id = reader.GetInt32(reader.GetOrdinal("Id")),
                        //    name = reader.GetString(reader.GetOrdinal("Department Name"))
                        //},
                        //     = new List<Employee>
                        //    {
                        //        firstName = reader.GetString(reader.GetOrdinal("Employee First Name")),
                        //        lastName = reader.GetString(reader.GetOrdinal("Employee Last Name"))
                        //    }
                        //);
                //    }
                //    reader.Close();
                //}
            //}
            //return something here - departmentEmployees or something else;
        //}

    }
}