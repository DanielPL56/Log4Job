using Log4Job.Models;
using System.Collections.Generic;

namespace Log4Job.ViewModels
{
    public class ProjectListViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}