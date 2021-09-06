using Log4Job.Models;

namespace Log4Job.ViewModels
{
    public class ProjectDetailsViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public Project Project { get; set; }
    }
}