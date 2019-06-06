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
        [Display(Name = "Training Start Date")]
        public DateTime startDate { get; set; }
        [Display(Name = "Training End Date")]
        public DateTime endDate { get; set; }
        public int maxAttendees { get; set; }
        public List<Employee> employeesInProgram { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if ((DateTime.Compare(endDate, startDate) < 0))
            {
                yield return new ValidationResult(
                    $"End date must be later than start date.",
                    new[] { "EndDate" });
            }
        }
    }
}
   