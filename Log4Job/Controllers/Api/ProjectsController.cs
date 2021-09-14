using AutoMapper;
using Log4Job.App_Start;
using Log4Job.DTOs;
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
        private IMapper _mapper;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
            _mapper = MappingProfile.CreateMapper();
        }

        [HttpGet]
        public IEnumerable<ProjectDTO> GetProjects()
        {
            using (_context)
            {
                return Projects().ToList().Select(p => _mapper.Map<ProjectDTO>(p));
            }
        }

        [HttpGet]
        public ProjectDTO GetProject(int id)
        {
            using (_context)
            {
                var project = Projects().SingleOrDefault(p => p.ProjectId == id);

                if (project == null)
                    throw new HttpResponseException(HttpStatusCode.NotFound);

                return _mapper.Map<ProjectDTO>(project);
            }
        }

        [HttpPost]
        public ProjectDTO CreateProject(ProjectDTO projectDTO)
        {
            CheckModelValid();

            using (_context)
            {
                var project = _mapper.Map<Project>(projectDTO);

                _context.Projects.Add(project);
                _context.SaveChanges();

                projectDTO.ProjectId = project.ProjectId;

                return projectDTO;
            }
        }

        [HttpPut]
        public void UpdateProject(int id, ProjectDTO projectDTO)
        {
            CheckModelValid();

            using (_context)
            {
                var projectInDb = Projects().SingleOrDefault(p => p.ProjectId == id);

                CheckProjectExist(projectInDb);

                _mapper.Map(projectDTO, projectInDb);

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
