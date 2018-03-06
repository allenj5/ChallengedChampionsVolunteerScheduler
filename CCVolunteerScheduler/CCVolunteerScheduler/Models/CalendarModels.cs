using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace CCVolunteerScheduler.Models
{
    public class CalendarViewModel
    {
        public int NumberOfDays { get; set; }
        public DateTime StartDate { get; set; }
        public int DaysInMonth { get; set; }
    }
}
