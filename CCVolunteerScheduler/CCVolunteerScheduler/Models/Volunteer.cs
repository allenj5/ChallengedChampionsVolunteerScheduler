
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

    public int ID { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public Nullable<int> Phone { get; set; }

    public string Email { get; set; }

    public bool Active { get; set; }

    public Nullable<int> HoursWorked { get; set; }

    public bool EmailOptOut { get; set; }

    public Nullable<bool> isAdmin { get; set; }

    public string Password { get; set; }

}

}
