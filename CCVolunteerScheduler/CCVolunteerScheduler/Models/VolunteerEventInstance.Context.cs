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
    
    public partial class VolunteerEventInstanceDBEntities : DbContext
    {
        public VolunteerEventInstanceDBEntities()
            : base("name=VolunteerEventInstanceDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
    
        [EdmFunction("VolunteerEventInstanceDBEntities", "VolunteerEvents")]
        public virtual IQueryable<VolunteerEvents_Result> VolunteerEvents(Nullable<long> volunteerID)
        {
            var volunteerIDParameter = volunteerID.HasValue ?
                new ObjectParameter("volunteerID", volunteerID) :
                new ObjectParameter("volunteerID", typeof(long));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<VolunteerEvents_Result>("[VolunteerEventInstanceDBEntities].[VolunteerEvents](@volunteerID)", volunteerIDParameter);
        }
    }
}