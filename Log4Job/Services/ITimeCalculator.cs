using Log4Job.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Job.Services
{
    public interface ITimeCalculator
    {
        double TotalHoursWorked(TimeReport timeReport);

        double TotalHoursWorked(ICollection<TimeReport> timeReports);

        double TotalHoursInProject(ICollection<TimeReport> timeReports, Project project);
    }
}