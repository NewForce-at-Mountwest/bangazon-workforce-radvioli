using BangazonWorkforce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models.ViewModels
{
    public class DepartmentEmployeesViewModel
    {
        public List<Employee> DepartmentEmployees { get; set; }

        public Department Department { get; set; }

        private string _connectionString;

        private SqlConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public DepartmentEmployeesViewModel(string connectionString)
        {
            _connectionString = connectionString;
            //TODO: need to call methods from respositories to Get Single Department by Id with employees.
        }

        
    }
}
