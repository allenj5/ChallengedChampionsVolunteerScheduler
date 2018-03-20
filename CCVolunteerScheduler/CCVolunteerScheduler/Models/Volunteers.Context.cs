﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;


public partial class VolunteersDBEntities : DbContext
{
    public VolunteersDBEntities()
        : base("name=VolunteersDBEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public DbSet<Volunteer> Volunteers { get; set; }

    public DbSet<Event> Events { get; set; }


    public virtual ObjectResult<string> Validate_User(Nullable<int> userID, string password)
    {

        var userIDParameter = userID.HasValue ?
            new ObjectParameter("UserID", userID) :
            new ObjectParameter("UserID", typeof(int));


        var passwordParameter = password != null ?
            new ObjectParameter("Password", password) :
            new ObjectParameter("Password", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Validate_User", userIDParameter, passwordParameter);
    }


    public virtual ObjectResult<string> ValidateUser(Nullable<int> userID, string password)
    {

        var userIDParameter = userID.HasValue ?
            new ObjectParameter("UserID", userID) :
            new ObjectParameter("UserID", typeof(int));


        var passwordParameter = password != null ?
            new ObjectParameter("Password", password) :
            new ObjectParameter("Password", typeof(string));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("ValidateUser", userIDParameter, passwordParameter);
    }


    public virtual ObjectResult<string> Check_Admin(Nullable<int> userID)
    {

        var userIDParameter = userID.HasValue ?
            new ObjectParameter("UserID", userID) :
            new ObjectParameter("UserID", typeof(int));


        return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("Check_Admin", userIDParameter);
    }

}

}

