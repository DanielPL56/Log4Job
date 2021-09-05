using Log4Job.Models;
using Log4Job.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using static Log4Job.ViewModels.UserDetailViewModel;

namespace Log4Job.Controllers
{
    [Authorize(Roles = RoleName.Admin)]
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _context;

        public EmployeeController()
        {
            _context = new ApplicationDbContext();
        }

        
        public ActionResult List()
        {
            using (_context)
            {
                var users = _context.Users.Include(u => u.Employee).Include(u => u.Employee.Projects).Include(u => u.Employee.TimeReports);
                var usersWithEmployees = users.Where(u => u.Employee != null).ToList();

                return View(usersWithEmployees);
            }
        }

        public ActionResult UserDetail(string id)
        {
            using (_context)
            {
                var userDetailViewModel = new UserDetailViewModel
                {
                    User = _context.Users.Include(u => u.Employee.TimeReports).SingleOrDefault(u => u.Id == id),
                    Months = GetMonths(),
                    WorkTimeCalculator = new WorkTimeCalculator(),
                    DateCalculator = new DateCalculator()
                };

                return View(userDetailViewModel);
            }
        }

        public ActionResult HoursByMonth(UserDetailViewModel entryViewModel)
        {
            using (_context)
            {
                var userDetailViewModel = new UserDetailViewModel()
                {
                    User = _context.Users.Include(u => u.Employee.TimeReports).SingleOrDefault(u => u.Id == entryViewModel.User.Id),
                    Months = GetMonths(),
                    ChoosenMonth = entryViewModel.ChoosenMonth,
                    WorkTimeCalculator = new WorkTimeCalculator(),
                    DateCalculator = new DateCalculator()
                };

                return View(userDetailViewModel);
            }
        }

        public ActionResult HoursByWeek(UserDetailViewModel entryViewModel)
        {
            using (_context)
            {
                if (entryViewModel.ChoosenWeek != null && entryViewModel.FilterStartDate != null && entryViewModel.FilterEndDate != null)
                {
                    var userDetailViewModel = new UserDetailViewModel()
                    {
                        User = _context.Users.Include(u => u.Employee.TimeReports).SingleOrDefault(u => u.Id == entryViewModel.User.Id),
                        Months = GetMonths(),
                        ChoosenMonth = entryViewModel.ChoosenMonth,
                        WorkTimeCalculator = new WorkTimeCalculator(),
                        DateCalculator = new DateCalculator()
                    };

                    var datesArray = entryViewModel.ChoosenWeek.Split('-');

                    userDetailViewModel.FilterStartDate = DateTime.Parse(datesArray[0]);
                    userDetailViewModel.FilterEndDate = DateTime.Parse(datesArray[1]);

                    return View(userDetailViewModel);
                }
            }

            return HttpNotFound();
        }

        public ActionResult NewEmployee()
        {
            return View();
        }

        public ActionResult Edit(string id)
        {
            using (_context)
            {
                var user = _context.Users.Include(u => u.Employee).SingleOrDefault(u => u.Id == id);

                return View(user);
            }
        }

        public ActionResult EditEmployee(ApplicationUser user)
        {
            using (_context)
            {
                var userInDb = _context.Users.Include(u => u.Employee).SingleOrDefault(u => u.Id == user.Id);

                userInDb.Employee.Name = user.Employee.Name;
                userInDb.Email = user.Email;
                userInDb.UserName = user.Email;

                _context.SaveChanges();
            }

            return RedirectToAction("List", "Employee");
        }

        public ActionResult Delete(string id)
        {
            using (_context)
            {
                var userToDelete = _context.Users.Include(u => u.Employee.TimeReports).SingleOrDefault(u => u.Id == id);

                _context.Employees.Remove(userToDelete.Employee);
                _context.Users.Remove(userToDelete);

                _context.SaveChanges();
            }

            return RedirectToAction("List", "Employee");
        }

        private IEnumerable<Month> GetMonths()
        {
            var months = new List<Month>
            {
                Month.January,
                Month.February,
                Month.March,
                Month.April,
                Month.May,
                Month.June,
                Month.July,
                Month.August,
                Month.September,
                Month.October,
                Month.November,
                Month.December
            };

            return months;
        }
    }
}