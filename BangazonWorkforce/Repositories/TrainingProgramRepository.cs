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

                        tp.Id, tp.Name AS 'Trng Prgm Name',
                        tp.StartDate AS 'Trng Prgm Start',
                        tp.EndDate AS 'Trng Prgm End',
                        tp.MaxAttendees AS 'Max Attendees'
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

        public static TrainingProgram GetOneProgram(int id)
        {
            List<Employee> ProgramEmployees = new List<Employee>();


            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"
                      SELECT tp.Id,
                              tp.Name AS 'Name',
                              tp.StartDate AS 'Start Date',
                              tp.EndDate AS 'End Date',
                              tp.MaxAttendees AS 'Max Attendees',
                              e.Id AS 'Employee Id', 
                              e.firstName AS 'First Name', 
                              e.LastName AS 'Last Name', 
                              e.DepartmentId AS 'Department Id' 
                              FROM EmployeeTraining et
                              FULL JOIN Employee e on et.EmployeeId = e.id 
                              FULL JOIN TrainingProgram tp on et.TrainingProgramId = tp.id 
                              WHERE tp.Id = @id";

                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    TrainingProgram trainingToDisplay = null;
                    while (reader.Read())
                    {
                        if (trainingToDisplay == null)
                        {
                            trainingToDisplay = new TrainingProgram
                            {
                                id = reader.GetInt32(reader.GetOrdinal("Id")),
                                name = reader.GetString(reader.GetOrdinal("Name")),
                                startDate = reader.GetDateTime(reader.GetOrdinal("Start Date")),
                                endDate = reader.GetDateTime(reader.GetOrdinal("End Date")),
                                maxAttendees = reader.GetInt32(reader.GetOrdinal("Max Attendees")),
                                employeesInProgram = new List<Employee>()
                            };
                        };
                        //adds an employee if it exists to the trainings employee list
                        if (!reader.IsDBNull(reader.GetOrdinal("Employee Id")))
                        {
                            Employee employee = new Employee()
                            {
                                id = reader.GetInt32(reader.GetOrdinal("Employee Id")),
                                firstName = reader.GetString(reader.GetOrdinal("First Name")),
                                lastName = reader.GetString(reader.GetOrdinal("Last Name")),
                                DepartmentId = reader.GetInt32(reader.GetOrdinal("Department Id"))

                            };
                            trainingToDisplay.employeesInProgram.Add(employee);
                        }
                    }
                    reader.Close();

                    return (trainingToDisplay);
                }
            }
        }


        public static void CreateTrainingProgram(TrainingProgram model)
        {

using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO TrainingProgram
                                       (name, startDate, endDate, maxAttendees)
                                        VALUES (@name, @startDate, @endDate, @maxAttendees)";
                    cmd.Parameters.Add(new SqlParameter("@name", model.name));
                    cmd.Parameters.Add(new SqlParameter("@startDate", model.startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", model.endDate));
                    cmd.Parameters.Add(new SqlParameter("@maxAttendees", model.maxAttendees));
                    cmd.ExecuteNonQuery();
                }
            }
        }
        //Getting single training program with their department
        public static TrainingProgram GetOneTrainingProgram(int id)
        {
            TrainingProgram trainingProgram = GetOneProgram(id);           
            return trainingProgram;
        }

        //edit a program

        public static void EditProgram(int id, TrainingProgram trainingProgram)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    string command = @"UPDATE TrainingProgram
                                    SET 
                                    name=@name, 
                                    startDate=@startDate, 
                                    endDate=@endDate,
                                    maxAttendees=@maxAttendees
                                    WHERE id=@id";

                    

                    cmd.CommandText = command;
                    cmd.Parameters.Add(new SqlParameter("@name", trainingProgram.name));
                    cmd.Parameters.Add(new SqlParameter("@startDate", trainingProgram.startDate));
                    cmd.Parameters.Add(new SqlParameter("@endDate", trainingProgram.endDate));
                    cmd.Parameters.Add(new SqlParameter("@maxAttendees", trainingProgram.maxAttendees));
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    int rowsAffected = cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

