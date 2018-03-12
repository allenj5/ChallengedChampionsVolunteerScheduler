using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCVolunteerScheduler.Models;

namespace CCVolunteerScheduler.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AdminCalendar()
        {
            Models.CalendarViewModel model = new Models.CalendarViewModel
            {
                NumberOfDays = 7,
                StartDate = DateTime.Now,
                DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year,DateTime.Now.Month)
            };
            return View(model);
        }

        public ActionResult AdminHome()
        {

            return View();
        }

        public ActionResult Communications()
        {

            return View();
        }

        public ActionResult ManageAccount()
        {

            return View();
        }

        public ActionResult MySchedule()
        {
            Models.CalendarViewModel model = new Models.CalendarViewModel
            {
                NumberOfDays = 7,
                StartDate = DateTime.Now,
                DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)
            };
            return View(model);
        }

        public ActionResult VolunteerCalendar()
        {
            Models.CalendarViewModel model = new Models.CalendarViewModel
            {
                NumberOfDays = 7,
                StartDate = DateTime.Now,
                DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)
            };
            return View(model);
        }


        public ActionResult ChangePassword()
        {
            return View();
        }

        //ComputeHash code will be needed here to check the old password, and to hash/salt the new password entered
        /* 
             -make sure using CCVolunteerScheduler.Models is called in your file
             -To hash the old password to check the database and hash the new password, add these 2 lines of code to your function:
         
                var salt = new Byte[16];
                string hashedPassword = HashingSaltModel.ComputeHash(user.Password, new SHA256CryptoServiceProvider(), salt);
        
              
         */

        public ActionResult ChangeEmailAddress()
        {
            return View();
        }

        public ActionResult ChangePhoneNumber()
        {
            return View();
        }



        public ActionResult ForgotPassword()
        {
            return View();
        }

        public ActionResult Volunteers()
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            ViewData.Model = _db.Volunteers.ToList();
            return View();
        }

        //function for volunteer table to Hash passwords for new accounts added by the admin
        /*         
              -make sure using CCVolunteerScheduler.Models is called in your file
              -To hash the password for the new account before adding it to the database, add these two lines of code to your function:
         
              var salt = new Byte[16];
              string hashedPassword = HashingSaltModel.ComputeHash(user.Password, new SHA256CryptoServiceProvider(), salt);         
         */



    }
}
