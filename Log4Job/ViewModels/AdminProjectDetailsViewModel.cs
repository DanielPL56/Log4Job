using Log4Job.Models;
using System.Collections.Generic;

namespace Log4Job.ViewModels
{
    public class AdminProjectDetailsViewModel
    {
        public ICollection<Employee> Employees { get; set; }
        public ICollection<TimeReport> TimeReportsForThisProject { get; set; }
        public Project CurrentProject { get; set; }
        public WorkTimeCalculator WorkTimeCalculator { get; set; }
    }
}