using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CCVolunteerScheduler.Models
{
    public partial class User
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Username required.")]
        public int Username { get; set; }

        [Required(ErrorMessage = "Password required.")]
        public string Password { get; set; }
    }
}