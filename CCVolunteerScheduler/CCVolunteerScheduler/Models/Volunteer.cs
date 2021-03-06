//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CCVolunteerScheduler.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Volunteer
    {
        public Volunteer()
        {
            this.Events = new HashSet<Event>();
        }
    
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
    
        public virtual ICollection<Event> Events { get; set; }
    }
}
