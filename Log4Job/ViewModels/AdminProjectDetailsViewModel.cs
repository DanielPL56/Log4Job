using Log4Job.Models;
using Log4Job.Services;
using System.Collections.Generic;

namespace Log4Job.ViewModels
{
    public class AdminProjectDetailsViewModel
    {
        public ICollection<Employee> Employees { get; set; }
        public ICollection<TimeReport> TimeReportsForThisProject { get; set; }
        public Project CurrentProject { get; set; }
        public ITimeCalculator WorkTimeCalculator { get; set; }
    }
}