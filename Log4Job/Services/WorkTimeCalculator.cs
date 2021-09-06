using System.Collections.Generic;

namespace Log4Job.Models
{
    public class WorkTimeCalculator
    {
        public double TotalHoursWorked(TimeReport timeReport)
        {
            return (timeReport.EndDate.GetValueOrDefault() - timeReport.StartDate.GetValueOrDefault()).TotalHours;
        }

        public double TotalHoursWorked(ICollection<TimeReport> timeReports)
        {
            double sumHours = 0.0;

            foreach (var timeReport in timeReports)
            {
                sumHours += TotalHoursWorked(timeReport);
            }

            return sumHours;
        }

        public double TotalHoursInProject(ICollection<TimeReport> timeReports, Project project)
        {
            double sumHours = 0.0;

            foreach (var timeReport in timeReports)
            {
                if(timeReport.Project != null && timeReport.Project.ProjectId == project.ProjectId)
                {
                    sumHours += TotalHoursWorked(timeReport);
                }
            }

            return sumHours;
        }
    }
}