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

        [HttpPost]
        public ActionResult AdminCalendar(string requestedDate, string userMonth)
        {
            int month = 0;

            switch(userMonth)
            {
                case "January":
                    month = 1;
                    break;
                case "February":
                    month = 2;
                    break;
                case "March":
                    month = 3;
                    break;
                case "April":
                    month = 4;
                    break;
                case "May":
                    month = 5;
                    break;
                case "June":
                    month = 6;
                    break;
                case "July":
                    month = 7;
                    break;
                case "August":
                    month = 8;
                    break;
                case "September":
                    month = 9;
                    break;
                case "October":
                    month = 10;
                    break;
                case "November":
                    month = 11;
                    break;
                case "December":
                    month = 12;
                    break;

            }

            DateTime newDate = new DateTime(DateTime.Now.Year, month, DateTime.Now.Day);

            if(requestedDate == "Back One Month")
            {
                newDate = newDate.AddMonths(-1);
            }
            else if(requestedDate == "Forward One Month")
            {
                newDate = newDate.AddMonths(1);
            }

            Models.CalendarViewModel model = new Models.CalendarViewModel
            {
                NumberOfDays = 7,
                StartDate = newDate,
                DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month),
                Month = newDate
            };
            return View("AdminCalendar", model);
        }

        [HttpGet]
        public ActionResult AdminCalendar()
        {
            Models.CalendarViewModel model = new Models.CalendarViewModel
            {
                NumberOfDays = 7,
                StartDate = DateTime.Now,
                DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month),
                Month = DateTime.Now
            };
            return View("AdminCalendar", model);
        }


        public ActionResult AdminGetEventsForDay(string day)
        {
            EventDBEntities _db = new EventDBEntities();
            var EventList = _db.Events.ToList();
            var Model = EventList.Where(x => x.EventDate.ToString("yyyy-MM-dd") == day);
            return PartialView(Model);
        }
        public ActionResult GetEventDetail(int id)
        {
            EventDBEntities _db = new EventDBEntities();
            var volunteerList = _db.Events.ToList();
            var Model = volunteerList.Where(x => x.EventID == id).FirstOrDefault();
            return PartialView("CopyEventPartial", Model);
        }
        public ActionResult AddEvent(string eventTitle, string eventDesc, String eventDate, String eventStart, String eventEnd){
            InsertEvent x = new InsertEvent();
            DateTime date = DateTime.Parse(eventDate);
            TimeSpan start = TimeSpan.Parse(eventStart);
            TimeSpan end = TimeSpan.Parse(eventEnd);
            x.Insert_Event(eventTitle, eventDesc, date, start, end);
            return View();
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
        public ActionResult MyScheduleGetEventsForDay(string day)
        {
            EventDBEntities _db = new EventDBEntities();
            var EventList = _db.Events.ToList();
            var Model = EventList.Where(x => x.EventDate.ToString("yyyy-MM-dd") == day);
            return PartialView("VolunteerEventsForDay", Model);
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
        public ActionResult VolunteerGetEventsForDay(string day)
        {
            EventDBEntities _db = new EventDBEntities();
            var EventList = _db.Events.ToList();
            var Model = EventList.Where(x => x.EventDate.ToString("yyyy-MM-dd") == day);
            return PartialView("VolunteerEventsForDay", Model);
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
        public ActionResult GetDetail(int id)
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            var volunteerList = _db.Volunteers.ToList();
            var Model = volunteerList.Where(x => x.ID == id).FirstOrDefault();
            return PartialView("EditVolunteerPartial", Model);
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
