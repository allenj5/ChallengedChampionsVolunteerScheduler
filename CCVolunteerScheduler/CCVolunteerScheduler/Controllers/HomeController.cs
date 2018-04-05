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
        public ActionResult AdminCalendar(string requestedDate, string userMonth, string userYear)
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

            DateTime newDate = new DateTime(Int32.Parse(userYear), month, DateTime.Now.Day);

            if(requestedDate == "Back One Month")
            {
                newDate = newDate.AddMonths(-1);
                if (month == 1)
                    newDate.AddYears(-1);
            }
            else if(requestedDate == "Forward One Month")
            {
                newDate = newDate.AddMonths(1);
                if (month == 12)
                    newDate.AddYears(1);

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


        public ActionResult AdminEventsForDay(string day)
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
        public ActionResult AddEvent(string title, string description, string date, string start, string end){

            DateTime myDate = DateTime.Parse(date);
            TimeSpan myStart = TimeSpan.Parse(start);
            TimeSpan myEnd = TimeSpan.Parse(end);

            EventSPEntities x = new EventSPEntities();
            x.Insert_Event(title, description, myDate, myStart, myEnd);

            return new EmptyResult();
        }
        public ActionResult DeleteEvent(int id)
        {
            EventSPEntities x = new EventSPEntities();
            x.Delete_Event(id);

            return new EmptyResult();
        }
        public ActionResult UpdateEvent(int id, string title, string description, string date, string start, string end)
        {
            DateTime myDate = DateTime.Parse(date);
            TimeSpan myStart = TimeSpan.Parse(start);
            TimeSpan myEnd = TimeSpan.Parse(end);

            EventSPEntities x = new EventSPEntities();
            x.Update_Event(id, title, description, myDate, myStart, myEnd);

            return new EmptyResult();
        }

        [ChildActionOnly]
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
        
        [HttpPost]
        public ActionResult MySchedule(string requestedDate, string userMonth, string userYear)
        {
            int month = 0;

            switch (userMonth)
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

            DateTime newDate = new DateTime(Int32.Parse(userYear), month, DateTime.Now.Day);

            if (requestedDate == "Back One Month")
            {
                newDate = newDate.AddMonths(-1);
                if (month == 1)
                    newDate.AddYears(-1);
            }
            else if (requestedDate == "Forward One Month")
            {
                newDate = newDate.AddMonths(1);
                if (month == 12)
                    newDate.AddYears(1);

            }

            Models.CalendarViewModel model = new Models.CalendarViewModel
            {
                NumberOfDays = 7,
                StartDate = newDate,
                DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month),
                Month = newDate
            };
            return View("MySchedule", model);
        }

        [HttpGet]
        public ActionResult MySchedule()
        {
            Models.CalendarViewModel model = new Models.CalendarViewModel
            {
                NumberOfDays = 7,
                StartDate = DateTime.Now,
                DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month),
                Month = DateTime.Now
            };
            return View("MySchedule", model);
        }
        public ActionResult MyScheduleGetEventsForDay(string day)
        {
            EventDBEntities _db = new EventDBEntities();
            var EventList = _db.Events.ToList();
            var Model = EventList.Where(x => x.EventDate.ToString("yyyy-MM-dd") == day);
            return PartialView("VolunteerEventsForDay", Model);
        }

        [HttpPost]
        public ActionResult VolunteerCalendar(string requestedDate, string userMonth, string userYear)
        {
            int month = 0;

            switch (userMonth)
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

            DateTime newDate = new DateTime(Int32.Parse(userYear), month, DateTime.Now.Day);

            if (requestedDate == "Back One Month")
            {
                newDate = newDate.AddMonths(-1);
                if (month == 1)
                    newDate.AddYears(-1);
            }
            else if (requestedDate == "Forward One Month")
            {
                newDate = newDate.AddMonths(1);
                if (month == 12)
                    newDate.AddYears(1);

            }

            Models.CalendarViewModel model = new Models.CalendarViewModel
            {
                NumberOfDays = 7,
                StartDate = newDate,
                DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month),
                Month = newDate
            };
            return View("VolunteerCalendar", model);
        }

        [HttpGet]
        public ActionResult VolunteerCalendar()
        {
            Models.CalendarViewModel model = new Models.CalendarViewModel
            {
                NumberOfDays = 7,
                StartDate = DateTime.Now,
                DaysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month),
                Month = DateTime.Now
            };
            return View("VolunteerCalendar", model);
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

        public ActionResult Volunteers()
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            ViewData.Model = _db.Volunteers.ToList();
            return View();
        }
        public ActionResult AddVolunteer(string firstName, string lastName, string phone, string email, string position)
        {
            var Hashing = new LoginController();

            int hoursWorked = 0;
            string password = Hashing.HashPassword("ChalChampVolunteer"); //should change this

            AddVolunteerEntities x = new AddVolunteerEntities();
            x.Insert_Volunteer(firstName, lastName, phone, email, hoursWorked, password, position);

            return new EmptyResult();
        }
        public ActionResult UpdateVolunteer(string id, string firstName, string lastName, string phone, string email, string active, string hoursWorked, string position)
        {
            int myID = Int32.Parse(id);
            int myHoursWorked = Int32.Parse(hoursWorked);

            bool myActive = false;
            if (active == "True" || active == "true")
            {
                myActive = true;
            }

            UpdateVolunteer x = new UpdateVolunteer();
            x.Update_Volunteer(myID, firstName, lastName, phone, email, myActive, myHoursWorked, position);

            return new EmptyResult();
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
