
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace BangazonWorkforce.Models.ViewModels
{
    public class CreateEmployeeViewModel
    {

        // This is where our dropdown options will go! SelectListItem is a built in type for dropdown lists
        public List<SelectListItem> Departments { get; set; }

        // An individual employee. When we render the form (i.e. make a GET request to Employee/Create) this will be null. When we submit the form (i.e. make a POST request to Employee/Create), this will hold the data from the form.
        public Employee Employee { get; set; }

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
            Departments = GetDepartments()
                .Select(department => new SelectListItem()
                {
                    Text = $"{ department.firstName } { department.lastName }",
                    Value = department.id.ToString()


                })
                .ToList();

            // Add an option with instructiosn for how to use the dropdown
            Department.Insert(0, new SelectListItem
            {
                Text = "Choose a Department",
                Value = "0"
            });

        }

        // Internal method -- connects to DB, gets all cohorts, returns list of cohorts
        protected List<Department> GetDepartments()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT id, name FROM Departments";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Department> department = new List<Department>();
                    while (reader.Read())
                    {
                        department.Add(new Department
                        {
                            id = reader.GetInt32(reader.GetOrdinal("id")),
                            name = reader.GetString(reader.GetOrdinal("name")),
                        });
                    }

                    reader.Close();

                    return department;
                }
            }
        }
    }
}
