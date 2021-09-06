using Log4Job.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Log4Job.ViewModels
{
    public class UserDetailViewModel
    {
        public ApplicationUser User { get; set; }
        public WorkTimeCalculator WorkTimeCalculator { get; set; }
        public IEnumerable<Month> Months { get; set; }

        [Display(Name = "Filter by Month")]
        public Month ChoosenMonth { get; set; }
        public DateCalculator DateCalculator { get; set; }

        [Display(Name = "Filter by Week")]
        public string ChoosenWeek { get; set; }
        public DateTime FilterStartDate { get; set; }
        public DateTime FilterEndDate { get; set; }
    }
}