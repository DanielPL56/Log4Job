using System.Collections.Generic;

namespace Log4Job.DTOs
{
    public class ProjectDTO
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<EmployeeDTO> Employees { get; set; }
        public ICollection<TimeReportDTO> TimeReports { get; set; }
    }
}