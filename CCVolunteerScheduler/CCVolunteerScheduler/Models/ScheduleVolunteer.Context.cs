﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class ScheduleVolunteerDBEntities : DbContext
    {
        public ScheduleVolunteerDBEntities()
            : base("name=ScheduleVolunteerDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        public virtual int Schedule_Volunteer(Nullable<int> volunteerID, Nullable<int> eventID)
        {
            var volunteerIDParameter = volunteerID.HasValue ?
                new ObjectParameter("VolunteerID", volunteerID) :
                new ObjectParameter("VolunteerID", typeof(int));
    
            var eventIDParameter = eventID.HasValue ?
                new ObjectParameter("EventID", eventID) :
                new ObjectParameter("EventID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("Schedule_Volunteer", volunteerIDParameter, eventIDParameter);
        }
    
        public virtual int unSchedule_Volunteer(Nullable<int> volunteerID, Nullable<int> eventID)
        {
            var volunteerIDParameter = volunteerID.HasValue ?
                new ObjectParameter("VolunteerID", volunteerID) :
                new ObjectParameter("VolunteerID", typeof(int));
    
            var eventIDParameter = eventID.HasValue ?
                new ObjectParameter("EventID", eventID) :
                new ObjectParameter("EventID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("unSchedule_Volunteer", volunteerIDParameter, eventIDParameter);
        }
    }
}