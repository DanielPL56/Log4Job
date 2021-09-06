using Log4Job.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Log4Job.UnitTests
{
    [TestClass]
    public class DateCalculatorTests
    {
        [TestMethod]
        public void WeeksInYear_GenerateWeeksInStrings_ReturnsIEnumerableStrings()
        {
            var dateCalculator = new DateCalculator();

            var result = dateCalculator.WeeksInYear();

            Assert.IsInstanceOfType(result, typeof(IEnumerable<string>));
        }
    }
}
