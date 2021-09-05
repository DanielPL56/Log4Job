using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Job.Models
{
    public class DateCalculator
    {
        public IEnumerable<string> WeeksInYear()
        {
            List<string> weeksPerYear = new List<string>();

            for (int currentMonth = 1; currentMonth <= 12; currentMonth++)
            {
                int daysInMonth = DateTime.DaysInMonth(DateTime.Today.Year, currentMonth);
                int weeks = daysInMonth / 7;
                int daysLeftFromWeek = daysInMonth % 7;

                int firstDay = 1;

                if (daysLeftFromWeek > 0) { weeks += 1; }

                for (int i = 0; i < weeks; i++)
                {
                    DateTime date = new DateTime(2021, currentMonth, firstDay);

                    int offset = DayOfWeek.Monday - date.DayOfWeek;
                    DateTime lastMonday = date.AddDays(offset);
                    DateTime nextSunday = lastMonday.AddDays(6);

                    weeksPerYear.Add($"{lastMonday.ToShortDateString()} - {nextSunday.ToShortDateString()}");

                    firstDay += 7;
                }
            }

            return weeksPerYear;
        }
    }
}