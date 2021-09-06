using Log4Job.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Log4Job.UnitTests
{
    [TestClass]
    public class WorkTimeCalculatorTests
    {
        [TestMethod]
        public void TotalHoursWorked_CalculateTimeSpan_ReturnsHoursInDouble()
        {
            var workTimeCalculator = new WorkTimeCalculator();
            var timeReport = new TimeReport()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddHours(2)
            };

            var result = workTimeCalculator.TotalHoursWorked(timeReport);

            Assert.IsInstanceOfType(result, typeof(double));
            Assert.IsTrue(result == 2);
        }

        [TestMethod]
        public void TotalHoursWorked_CalculateTimeSpanFromICollection_ReturnSumHoursInDoble()
        {
            var workTimeCalculator = new WorkTimeCalculator();
            var timeReports = new List<TimeReport>()
            {
                new TimeReport { StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(2) },
                new TimeReport { StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(4) }
            };

            var result = workTimeCalculator.TotalHoursWorked(timeReports);

            Assert.IsInstanceOfType(result, typeof(double));
            Assert.IsTrue(result == 6);
        }
    }
}
