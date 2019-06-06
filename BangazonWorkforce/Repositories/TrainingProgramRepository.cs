using BangazonWorkforce.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Repositories
{
    public class TrainingProgramRepository
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
        //GET: Training Programs
        public static List<TrainingProgram> GetTrainingPrograms()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT 
                        tp.Id, tp.Name AS 'Trng Prgm Name', tp.StartDate AS 'Trng Prgm Start', tp.EndDate AS 'Trng Prgm End', tp.MaxAttendees AS 'Max Attendees'
                        FROM TrainingProgram tp
                        ";
                    SqlDataReader reader = cmd.ExecuteReader();

                    List<TrainingProgram> tpsReports = new List<TrainingProgram>();
                    while (reader.Read())
                    {
                        TrainingProgram tpsReport = new TrainingProgram
                        {
                            id = reader.GetInt32(reader.GetOrdinal("Id")),
                            name = reader.GetString(reader.GetOrdinal("Trng Prgm Name")),
                            startDate = reader.GetDateTime(reader.GetOrdinal("Trng Prgm Start")),
                            endDate = reader.GetDateTime(reader.GetOrdinal("Trng Prgm End")),
                            maxAttendees = reader.GetInt32(reader.GetOrdinal("Max Attendees"))
                        };

                        tpsReports.Add(tpsReport);
                    }

                    reader.Close();

                    return tpsReports;

                }
            }
        }
    }
}