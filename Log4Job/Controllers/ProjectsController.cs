using Log4Job.Models;
using Log4Job.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;

namespace Log4Job.Controllers
{
    public class ProjectsController : Controller
    {
        private ApplicationDbContext _context;

        public ProjectsController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize(Roles = RoleName.Admin)]
        public ActionResult New()
        {
            return View();
        }

        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create(Project project)
        {
            if (project.ProjectId == 0)
            {
                using (_context)
                {
                    _context.Projects.Add(project);
                    _context.SaveChanges();
                }

                return RedirectToAction("List", "Projects");
            }

            return RedirectToAction("Update", "Projects", project);
        }

        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Update(Project project)
        {
            using (_context)
            {
                var projectInDb = _context.Projects.SingleOrDefault(p => p.ProjectId == project.ProjectId);

                projectInDb.Name = project.Name;
                projectInDb.Description = project.Description;

                _context.SaveChanges();
            }

            return RedirectToAction("List", "Projects");
        }

        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int id)
        {
            using (_context)
            {
                _context.Projects.Remove(_context.Projects.Include(p => p.TimeReports).Include(p => p.Employees).SingleOrDefault(p => p.ProjectId == id));

                _context.SaveChanges();
            }

            return RedirectToAction("List", "Projects");
        }

        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id)
        {
            using (_context)
            {
                var project = _context.Projects.SingleOrDefault(p => p.ProjectId == id);

                if (project == null)
                    return HttpNotFound();

                return View("New", project);
            }
        }

        //[Authorize(Roles = RoleName.Admin)]
        public ActionResult List()
        {
            using (_context)
            {
                if (User.IsInRole(RoleName.Admin))
                {
                    return View("AdminList", _context.Projects.ToList());
                }

                var projectListViewModel = new ProjectListViewModel()
                {
                    CurrentUser = _context.Users.Include(u => u.Employee.Projects).SingleOrDefault(u => u.UserName == User.Identity.Name),
                    Projects = _context.Projects.Include(p => p.Employees).ToList()
                };

                return View("List", projectListViewModel);
            }
        }

        public ActionResult ProjectDetails(int id)
        {
            using (_context)
            {
                if (User.IsInRole(RoleName.Admin))
                {
                    var employees = _context.Employees.Include(e => e.Projects).Include(e => e.TimeReports).ToList();
                    var filteredEmployees = new List<Employee>();
                    var filteredTimeReports = new List<TimeReport>();

                    foreach (var employee in employees)
                    {
                        foreach (var employeeProject in employee.Projects.Where(p => p.ProjectId == id))
                        {
                            filteredEmployees.Add(employee);
                        }

                        foreach (var employeeTimeReport in employee.TimeReports)
                        {
                            if(employeeTimeReport.Project != null && employeeTimeReport.Project.ProjectId == id)
                                filteredTimeReports.Add(employeeTimeReport);
                        }
                    }

                    var adminViewModel = new AdminProjectDetailsViewModel()
                    {
                        Employees = filteredEmployees,
                        CurrentProject = _context.Projects.SingleOrDefault(p => p.ProjectId == id),
                        TimeReportsForThisProject = filteredTimeReports,
                        WorkTimeCalculator = new WorkTimeCalculator()
                    };

                    return View("AdminProjectDetails", adminViewModel);
                }

                var projectListViewModel = new ProjectDetailsViewModel()
                {
                    CurrentUser = _context.Users.Include(u => u.Employee.Projects).SingleOrDefault(u => u.UserName == User.Identity.Name),
                    Project = _context.Projects.Include(p => p.TimeReports).SingleOrDefault(p => p.ProjectId == id)
                };
                return View(projectListViewModel);
            }
        }

        //[Route("Join/{userName}")]
        public ActionResult AddProjectToList(int id)
        {
            using (_context)
            {
                var user = _context.Users.Include(u => u.Employee.Projects).SingleOrDefault(u => u.UserName == User.Identity.Name);

                user.Employee.Projects.Add(_context.Projects.SingleOrDefault(p => p.ProjectId == id));

                _context.SaveChanges();
            }

            return RedirectToAction("List", "Projects");
        }

        public ActionResult RemoveProjectFromList(int id)
        {
            using (_context)
            {
                var user = _context.Users.Include(u => u.Employee.Projects).SingleOrDefault(u => u.UserName == User.Identity.Name);

                user.Employee.Projects.Remove(_context.Projects.SingleOrDefault(p => p.ProjectId == id));

                _context.SaveChanges();
            }

            return RedirectToAction("List", "Projects");
        }

        public ActionResult StartTimeStamp(int id)
        {
            using (_context)
            {
                var currentUser = _context.Users.Include(u => u.Employee.TimeReports).SingleOrDefault(u => u.UserName == User.Identity.Name);
                var project = _context.Projects.Include(p => p.Employees).Include(p => p.TimeReports).SingleOrDefault(p => p.ProjectId == id);

                currentUser.Employee.TimeReports.Add(new TimeReport { StartDate = DateTime.Now, Project = project });

                _context.SaveChanges();
            }

            return RedirectToAction("ProjectDetails", new { id = id });
        }

        public ActionResult EndTimeStamp(int id)
        {
            using (_context)
            {
                var currentUser = _context.Users.Include(u => u.Employee.TimeReports).SingleOrDefault(u => u.UserName == User.Identity.Name);
                var project = _context.Projects.Include(p => p.Employees).Include(p => p.TimeReports).SingleOrDefault(p => p.ProjectId == id);

                foreach (var userTimeReport in currentUser.Employee.TimeReports)
                {
                    if (project.TimeReports.Contains(userTimeReport))
                    {
                        userTimeReport.EndDate = DateTime.Now;
                    }
                }

                _context.SaveChanges();
            }

            return RedirectToAction("ProjectDetails", new { id = id });
        }
    }
}