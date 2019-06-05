//using Microsoft.AspNetCore.Mvc.Rendering;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;


//namespace BangazonWorkforce.Models.ViewModels
//{
//    public class DeptCreateViewModel
//    {        
//        public Department department { get; set; }

//        protected string _connectionString;

//        public DeptCreateViewModel Connection
//        {
//            get
//            {
//                return new SqlConnection(_connectionString);
//            }
//        }

//        public DeptCreateViewModel() { }

//        public DeptCreateViewModel(string connectionString)
//        {
//            _connectionString = connectionString;

//            Cohorts = GetAllCohorts()
//                .Select(li => new SelectListItem
//                {
//                    Text = li.name,
//                    Value = li.id.ToString()
//                })
//                .ToList();

//            Cohorts.Insert(0, new SelectListItem
//            {
//                Text = "Choose cohort...",
//                Value = "0"
//            });
//        }

//        private List<Cohort> GetAllCohorts()
//        {
//            using (SqlConnection conn = Connection)
//            {
//                conn.Open();
//                using (SqlCommand cmd = conn.CreateCommand())
//                {
//                    cmd.CommandText = "SELECT id, name FROM Cohort";
//                    SqlDataReader reader = cmd.ExecuteReader();

//                    List<Cohort> cohorts = new List<Cohort>();
//                    if (reader.Read())
//                    {
//                        cohorts.Add(new Cohort
//                        {
//                            id = reader.GetInt32(reader.GetOrdinal("Id")),
//                            name = reader.GetString(reader.GetOrdinal("Name")),
//                        });
//                    }

//                    reader.Close();

//                    return cohorts;
//                }
//            }
//        }
//    }
//}