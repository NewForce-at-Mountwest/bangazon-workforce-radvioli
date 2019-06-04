using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangazonWorkforce.Models
{
    public class TrainingProgram
    {
        public int id { get; set; }

        [Display(Name = "Training Program Name")]
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int maxAttendees { get; set; }
        public List<Employee> employeesInTrainingProgram { get; set; } 
    }
}
