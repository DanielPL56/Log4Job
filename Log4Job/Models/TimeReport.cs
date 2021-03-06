using System;

namespace Log4Job.Models
{
    public class TimeReport
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Employee Employee { get; set; }
        public Project Project { get; set; }
    }
}