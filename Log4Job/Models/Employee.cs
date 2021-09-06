using System.Collections.Generic;

namespace Log4Job.Models
{
    public class Employee
    {
        public int? EmployeeId { get; set; }
        public string Name { get; set; }
        public ICollection<Project> Projects { get; set; }
        public ICollection<TimeReport> TimeReports { get; set; }
    }
}