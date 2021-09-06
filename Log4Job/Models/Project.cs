using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Log4Job.Models
{
    public class Project
    {
        public int ProjectId { get; set; }
        [Display(Name = "Project's Name")]
        public string Name { get; set; }
        [Display(Name = "Project's Description")]
        public string Description { get; set; }
        public ICollection<Employee> Employees { get; set; }
        public ICollection<TimeReport> TimeReports { get; set; }
    }
}