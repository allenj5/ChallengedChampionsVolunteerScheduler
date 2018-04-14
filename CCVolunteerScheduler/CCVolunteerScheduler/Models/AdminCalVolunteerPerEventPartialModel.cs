using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CCVolunteerScheduler.Models
{
    public class AdminCalVolunteerPerEventPartialModel
    {
        public int event_ID { get; set; }
        public IQueryable<VolunteersSignedUp_Result> volunteers { get; set; }
    }
}