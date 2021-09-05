using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Job.Models
{
    public class WorkTimeCalculator
    {
        public double SumHoursWorkedInMonth(ICollection<TimeReport> timeReports)
        {
            double sumHoursWorked = 0.0;

            foreach (var timeReport in timeReports)
            {
                sumHoursWorked += SumHoursWorked(timeReport);
            }

            return sumHoursWorked;
        }

        public double SumHoursInDay(TimeReport timeReport)
        {
            return SumHoursWorked(timeReport);
        }

        public double TotalHoursInProject(ICollection<TimeReport> timeReports, Project project)
        {
            double sumHoursWorked = 0.0;

            foreach (var timeReport in timeReports)
            {
                if(timeReport.Project != null &&timeReport.Project.ProjectId == project.ProjectId)
                {
                    sumHoursWorked += SumHoursWorked(timeReport);
                }
            }

            return sumHoursWorked;
        }

        private double SumHoursWorked(TimeReport timeReport)
        {
            return (timeReport.EndDate.GetValueOrDefault() - timeReport.StartDate.GetValueOrDefault()).TotalHours;
        }
    }
}