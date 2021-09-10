using Log4Job.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace Log4Job.Controllers.Api
{
    public class ProjectsController : ApiController
    {
        private ApplicationDbContext _context;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<Project> GetProjects()
        {
            using (_context)
            {
                return Projects();
            }
        }

        [HttpGet]
        public Project GetProject(int id)
        {
            using (_context)
            {
                var project = Projects().SingleOrDefault(p => p.ProjectId == id);

                if (project == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                return project;
            }
        }

        [HttpPost]
        public Project CreateProject(Project project)
        {
            CheckModelValid();

            using (_context)
            {
                _context.Projects.Add(project);
                _context.SaveChanges();

                return project;
            }
        }

        [HttpPut]
        public void UpdateProject(int id, Project project)
        {
            CheckModelValid();

            using (_context)
            {
                var projectInDb = Projects().SingleOrDefault(p => p.ProjectId == id);

                CheckProjectExist(projectInDb);

                projectInDb.Name = project.Name;
                projectInDb.Description = project.Description;

                _context.SaveChanges();
            }
        }

        [HttpDelete]
        public void DeleteProject(int id)
        {
            using (_context)
            {
                var projectInDb = Projects().SingleOrDefault(p => p.ProjectId == id);

                CheckProjectExist(projectInDb);

                _context.Projects.Remove(projectInDb);
                _context.SaveChanges();
            }
        }

        private IEnumerable<Project> Projects()
        {
            return _context.Projects.Include(p => p.Employees).Include(p => p.TimeReports).ToList();
        }

        private void CheckModelValid()
        {
            if (!ModelState.IsValid)
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        private void CheckProjectExist(Project project)
        {
            if (project == null)
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }
    }
}
