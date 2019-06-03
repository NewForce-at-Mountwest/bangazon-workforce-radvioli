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
