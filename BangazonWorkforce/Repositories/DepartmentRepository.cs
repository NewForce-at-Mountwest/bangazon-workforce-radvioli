using BangazonWorkforce.Models;
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