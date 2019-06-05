using BangazonWorkforce.Models;
using BangazonWorkforce.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data.SqlClient;

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
                        COUNT(e.Id) AS 'Department Size'
                        FROM Employee e
                        RIGHT JOIN Department d 
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
        public static void CreateDepartment(Department model)
        {
            using (SqlConnection conn = connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO Department
                ( name, budget )
                VALUES
                ( @name, @budget )";
                    cmd.Parameters.Add(new SqlParameter("@name", model.name));
                    cmd.Parameters.Add(new SqlParameter("@budget", model.budget));
                    cmd.ExecuteNonQuery();


                }
            }

        }
        //public static Department GetOneDepartmentWithEmployees(int id)
        //{
        //    using (SqlConnection conn = connection)
        //    {
        //        conn.Open();
        //        using (SqlCommand cmd = conn.CreateCommand())
        //        {
        //            cmd.CommandText = @"
        //            SELECT d.Id, 
        //                    d.Name AS 'Department Name',
        //                    d.Budget 
        //                    //e.firstName, 
        //                    //e.lastName
        //            FROM Department d
        //            JOIN Employee e ON e.departmentId = d.Id";
        //            cmd.Parameters.Add(new SqlParameter("@id", id));
        //            SqlDataReader reader = cmd.ExecuteReader();
        //            Department department = null;
        //            if (reader.Read())
        //            {
        //                department = new Department
        //                {
        //                    id = reader.GetInt32(reader.GetOrdinal("Id")),
        //                    name = reader.GetString(reader.GetOrdinal("Department Name")),
        //                    budget = reader.GetInt32(reader.GetOrdinal("Budget")),
        //                }
    }
}