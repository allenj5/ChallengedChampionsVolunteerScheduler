using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CCVolunteerScheduler.Models
{
    using System;
    using System.Collections.Generic;

    public class VolunteersSignedUp_Result
    {
        public long ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<long> HoursWorked { get; set; }
        public Nullable<bool> EmailOptOut { get; set; }
        public Nullable<bool> isAdmin { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public string GUID { get; set; }
    
    }
}
