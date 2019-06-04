﻿
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BangazonWorkforce.Models.ViewModels
{
    public class CreateEmployeeViewModel
    {

        // This is where our dropdown options will go! SelectListItem is a built in type for dropdown lists
        public List<SelectListItem> Cohorts { get; set; }

        // An individual employee. When we render the form (i.e. make a GET request to Employee/Create) this will be null. When we submit the form (i.e. make a POST request to Employee/Create), this will hold the data from the form.
        public Employee employee { get; set; }

        // Connection to the database
        protected string _connectionString;

        protected SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        // Empty constructor so that we can create a new instance of this view model when we make our POST request (in which case it won't need a connection string)
        public CreateEmployeeViewModel() { }

        // This is an example of method overloading! We have one constructor with no parameter and another constructor that's expecting a connection string. We can call either one!
        public CreateEmployeeViewModel(string connectionString)
        {
            _connectionString = connectionString;

            // When we create a new instance of this view model, we'll call the internal methods to get all the cohorts from the database
            // Then we'll map over them and convert the list of cohorts to a list of select list items
            employee = GetEmployees()
                .Select(employee => new SelectListItem()
                {
                    Text = cohort.name,
                    Value = cohort.id.ToString()

                })
                .ToList();

            // Add an option with instructiosn for how to use the dropdown
            Cohorts.Insert(0, new SelectListItem
            {
                Text = "Choose a cohort",
                Value = "0"
            });

        }

        // Internal method -- connects to DB, gets all cohorts, returns list of cohorts
        protected List<Employee> GetEmployees()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Cohort";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Employee> employees = new List<Employee>();
                    while (reader.Read())
                    {
                        employees.Add(new Employee
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            name = reader.GetString(reader.GetOrdinal("Name")),
                        });
                    }

                    reader.Close();

                    return cohorts;
                }
            }
        }
    }
}
