using Log4Job.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Job.ViewModels
{
    public class ProjectListViewModel
    {
        public ApplicationUser CurrentUser { get; set; }
        public ICollection<Project> Projects { get; set; }
    }
}