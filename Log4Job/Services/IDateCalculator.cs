using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4Job.Services
{
    public interface IDateCalculator
    {
        IEnumerable<string> WeeksInYear();
    }
}