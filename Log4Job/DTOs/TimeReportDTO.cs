using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Job.DTOs
{
    public class TimeReportDTO
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public EmployeeDTO Employee { get; set; }
        public ProjectDTO Project { get; set; }
    }
}