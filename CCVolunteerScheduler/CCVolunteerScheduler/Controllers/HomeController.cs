using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CCVolunteerScheduler.Models;

namespace CCVolunteerScheduler.Controllers
{
    [NoDirectAccess]
    public class HomeController : Controller
    {
        public static long currentUserId = 0;

        public static long currentUser
        {
            get
            {
                return currentUserId;
            }
            set
            {
                currentUserId = value;
            }
        }

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
            EventsPerDayWithNumVolunteerEntities _db = new EventsPerDayWithNumVolunteerEntities();
            var Model = _db.GetEventsForDay(DateTime.Parse(day));
            return PartialView(Model);
        }

        public ActionResult GetEventDetail(int id)
        {
            EventDBEntities _db = new EventDBEntities();
            var EventList = _db.Events.ToList();
            var Model = EventList.Where(x => x.EventID == id).FirstOrDefault();
            return PartialView("CopyEventPartial", Model);
        }
        public ActionResult SeeVolunteersSignedUp(int eventID)
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            var volunteerList = _db.VolunteersSignedUp(eventID);
            if (volunteerList.FirstOrDefault().FirstName == null)
            {
                throw new Exception();
            }

            Models.AdminCalVolunteerPerEventPartialModel Model = new Models.AdminCalVolunteerPerEventPartialModel
            {
                event_ID = eventID,
                volunteers = volunteerList
            };

            return PartialView("AdminCalVolunteerPerEventPartial", Model);
        }
        public ActionResult AddEvent(string title, string description, string date, string start, string end, string maxVolunteers){
            if (true)
            {
                try
                {
                    DateTime myDate = DateTime.Parse(date);
                    TimeSpan myStart = TimeSpan.Parse(start);
                    TimeSpan myEnd = TimeSpan.Parse(end);
                    int numVolunteers = Int32.Parse(maxVolunteers);

                    if (NameValidation(title) && DescriptionValidation(description))
                    {
                        EventSPEntities x = new EventSPEntities();
                        x.Insert_Event(title, description, myDate, myStart, myEnd, numVolunteers);
                    }
                    else
                    {
                        return Json(new { status = "error", message = "Make sure Title is less than 20 characters and Description is less than 50 characters" });
                    }
                }
                catch
                {
                    return Json(new { status = "error", message = "invalid information entered" });
                }
            }
            return new EmptyResult();
        }

        public ActionResult DeleteEvent(int id)
        {
            EventDBEntities _db = new EventDBEntities();
            var Model = _db.Events.Where(e => e.EventID == id).FirstOrDefault();
            //Set admin id here
            SendEmail("aldaghiria@findlay.edu", Model.EventTitle + " Deleted", "Your event has been deleted");
            Models.Event myEvent = (Models.Event)_db.Events.Where(y => y.EventID == id).FirstOrDefault();

            if (myEvent.EventDate >= DateTime.Now && myEvent.Volunteers != null)
            {
                var volunteers = myEvent.Volunteers.ToList();

                foreach (var volunteer in volunteers)
                {
                    AdminUnScheduleVolunteer((int)volunteer.ID, id);
                }
            }

            //have to do this AFTER emailing volunteers
            EventSPEntities x = new EventSPEntities();
            x.Delete_Event(id);

            return new EmptyResult();
        }

        public ActionResult UpdateEvent(int id, string title, string description, string date, string start, string end, string maxVolunteers)
        {
            try
            {
                DateTime myDate = DateTime.Parse(date);
                TimeSpan myStart = TimeSpan.Parse(start);
                TimeSpan myEnd = TimeSpan.Parse(end);
                int numVolunteers = Int32.Parse(maxVolunteers);

                if (NameValidation(title) && DescriptionValidation(description))
                {
                    EventSPEntities x = new EventSPEntities();
                    x.Update_Event(id, title, description, myDate, myStart, myEnd, numVolunteers);
                }
                else
                {
                    return Json(new { status = "error", message = "invalid information entered" });
                }
            }
            catch
            {
                return Json(new { status = "error", message = "invalid information entered" });
            }

            return new EmptyResult();
        }

        public ActionResult AdminHome(long id = -1)
        {
            if (id != -1)
            {
                currentUser = id;
            }

            VolunteersDBEntities _db = new VolunteersDBEntities();
            var volunteerList = _db.Volunteers.ToList();
            var Model = volunteerList.Where(x => x.ID == currentUserId).FirstOrDefault();

            return View(Model);
        }

        public ActionResult Communications()
        {

            return View();
        }

        [HttpPost]
        public JsonResult Communications(int EmailFlag, string subject, string message)
        {
            int counter = 0;
            using (VolunteersDBEntities _db = new VolunteersDBEntities())
            {
                List<Volunteer> volunteerList = new List<Volunteer>();
                if (EmailFlag == 1)
                {
                    volunteerList = _db.Volunteers.Where(v => v.Position == "Walker                   ").ToList();
                }
                else if (EmailFlag == 2)
                {
                    var currentDate = DateTime.Now.Date;
                    IQueryable<Event> eventList = _db.Events.Where(v => v.EventDate == currentDate);
                    foreach (var e in eventList)
                    {
                        Volunteer v = e.Volunteers.FirstOrDefault();
                        if (v != null)
                        {
                            volunteerList.Add(v);
                        }
                    }
                }

                foreach (Volunteer v in volunteerList)
                {
                    SendEmail(v.Email, subject, message);
                    counter++;
                }
            }

            return Json(counter);
        }

        public ActionResult ManageAccount()
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            var volunteerList = _db.Volunteers.ToList();
            var Model = volunteerList.Where(x => x.ID == currentUserId).FirstOrDefault();

            return View(Model);
        }
        public ActionResult ToggleAutoRemind()
        {
            ToggleAutoRemindAction x = new ToggleAutoRemindAction();
            x.ToggleAutoRemindOptOut(currentUserId);
            return new EmptyResult();
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
        public ActionResult MySchedule(long id = -1)
        {
            if (id != -1)
            {
                currentUser = id;
            }
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
            VolunteerEventInstanceDBEntities _db = new VolunteerEventInstanceDBEntities();
            var EventList = _db.VolunteerEvents(currentUser).ToList();
            var Model = EventList.Where(x => x.EventDate.ToString("yyyy-MM-dd") == day);
            return PartialView("MyScheduleEventsPerDay", Model);
        }

        
        public ActionResult UnScheduleVolunteer(int id)
        {

            EventDBEntities _db = new EventDBEntities();
            var Model = _db.Events.Where(e => e.EventID == id).FirstOrDefault();
            Volunteer v = Model.Volunteers.FirstOrDefault();

            //Set admin email here
            SendEmail("aldaghiria@findlay.edu", Model.EventTitle + " Decommitted", v.FirstName.Trim() + " " + v.LastName.Trim() + " has decommitted from the " + Model.EventTitle + " " + Model.EventDate.ToLongDateString() + " event");

            ScheduleVolunteerDBEntities x = new ScheduleVolunteerDBEntities();
            x.unSchedule_Volunteer(Convert.ToInt32(currentUser), id);     //we need to revisit inconsistencies in DB with bigint / int for id column datatypes
            return new EmptyResult();
        }

        public ActionResult AdminUnScheduleVolunteer(int volunteerID, int eventID)
        {
            ScheduleVolunteerDBEntities x = new ScheduleVolunteerDBEntities();
            x.unSchedule_Volunteer(volunteerID, eventID);

            EventDBEntities _Edb = new EventDBEntities();
            var events = _Edb.Events.ToList();
            Models.Event myEvent = (Models.Event)(events.Where(y => y.EventID == eventID).FirstOrDefault());

            VolunteersDBEntities _Vdb = new VolunteersDBEntities();
            var volunteers = _Vdb.Volunteers.ToList();
            Models.Volunteer myVolunteer = (Models.Volunteer)(volunteers.Where(z => z.ID == volunteerID).FirstOrDefault());

            //notify volunteer of schedule change
            SmtpClient client = new SmtpClient();
            MailMessage notifyVolunteer = new MailMessage();
            notifyVolunteer.Subject = "You have been unscheduled from an event at the Challenged Champions Equestrian Center";
            notifyVolunteer.Body = "This is an automated message informing you that an administrator has unscheduled you from the following event: "
                + myEvent.EventTitle + " on " + myEvent.EventDate.ToString("MM-dd-yyyy")
             + ". Please check the \"My Schedule\" portion of the volunteer website to review your schedule.";
            notifyVolunteer.To.Add(myVolunteer.Email);

            client.Send(notifyVolunteer);

            return new EmptyResult();
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
            EventsPerDayWithNumVolunteerEntities _db = new EventsPerDayWithNumVolunteerEntities();
            var Model = _db.GetEventsForDay(DateTime.Parse(day));
            return PartialView("VolunteerEventsForDay", Model);
        }
        public ActionResult ScheduleVolunteer(int id)
        {
            //Check if already signed up
            try
            {
                ScheduleVolunteerDBEntities x = new ScheduleVolunteerDBEntities();
                x.Schedule_Volunteer(Convert.ToInt32(currentUser), id);     //we need to revisit inconsistencies in DB with bigint / int for id column datatypes
                return new EmptyResult();
            }
            catch(Exception e)
            {
                return Json(new {status="error", message="already signed up for this event"});
            }
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
         [HttpGet]
        public ActionResult Volunteers()
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            ViewData.Model = _db.Volunteers.OrderByDescending(x => x.Active).ThenBy(x => x.Position).ToList();
            return View();
        }

        public ActionResult TextVolunteers()
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            ViewData.Model = _db.Volunteers.OrderByDescending(x => x.Active).ThenBy(x => x.Position).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Volunteers(int setHoursTo = 0)
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            _db.ResetHours();
            ViewData.Model = _db.Volunteers.OrderBy(x => x.Active).ThenByDescending(x => x.Position).ToList();
            return View();
        }

        public ActionResult AddVolunteer(string firstName, string lastName, string phone, string email, string position)
        {
            if (NameValidation(firstName) && NameValidation(lastName) && PhoneValidation(phone) && EmailValidation(email) && NameValidation(position))
            {
                var Hashing = new LoginController();

                int hoursWorked = 0;
                string password = Hashing.HashPassword("ChalChampVolunteer"); //should change this

                AddVolunteerEntities x = new AddVolunteerEntities();
                x.Insert_Volunteer(firstName, lastName, phone, email, hoursWorked, password, position);

                return new EmptyResult();
            }
            else
            {
                return new EmptyResult();
            }
        }
        public ActionResult UpdateVolunteer(string id, string firstName, string lastName, string phone, string email, string active, string hoursWorked, string position)
        {

            if (NumberValidation(id) && NameValidation(firstName) && NameValidation(lastName) && PhoneValidation(phone) && EmailValidation(email) && NumberValidation(hoursWorked) && NameValidation(position))
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
            else
            {
                return new EmptyResult();
            }
        }

        public ActionResult GetDetail(int id)
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            var volunteerList = _db.Volunteers.ToList();
            var Model = volunteerList.Where(x => x.ID == id).FirstOrDefault();
            return PartialView("EditVolunteerPartial", Model);
        }

        bool NumberValidation(string number)
        {
            var num = number.Replace(" ", String.Empty);
            int n;
            return int.TryParse(num, out n);
        }

        bool NameValidation(string name)
        {
            var trimmedName = name.Replace(" ", String.Empty);
            var nameRegex = new Regex("^[a-zA-Z0-9 ']*$");
            return nameRegex.IsMatch(trimmedName) && !trimmedName.Contains("' ") && trimmedName.Length < 20;
        }

        bool DescriptionValidation(string name)
        {
            var trimmedDesc = name.Replace(" ", String.Empty);
            var nameRegex = new Regex("^[a-zA-Z0-9 ']*$");
            return nameRegex.IsMatch(trimmedDesc) && !trimmedDesc.Contains("' ") && trimmedDesc.Length < 50;
        }

        bool PhoneValidation(string phone)
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            var cellPhone = digitsOnly.Replace(phone, "");
            return cellPhone.Length == 10;
        }

        bool EmailValidation(string email)
        {
            try
            {
                email = email.Replace(" ", String.Empty);
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private bool SendEmail(string To, string Subject, string Message)
        {
            try
            {
                var smtp = new SmtpClient();
                var credential = (System.Net.NetworkCredential)smtp.Credentials;
                using (var message = new MailMessage(credential.UserName.ToString(), To)
                {
                    Subject = Subject,
                    Body = Message
                })
                {
                    smtp.Send(message);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
