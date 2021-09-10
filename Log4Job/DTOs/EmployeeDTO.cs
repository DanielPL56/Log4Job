using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Job.DTOs
{
    public class EmployeeDTO
    {
        public int? EmployeeId { get; set; }
        public string Name { get; set; }
        public ICollection<ProjectDTO> Projects { get; set; }
        public ICollection<TimeReportDTO> TimeReports { get; set; }
    }
}