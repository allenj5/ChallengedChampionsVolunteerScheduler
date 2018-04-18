using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using CCVolunteerScheduler.Models;
using System.Web.Security;
using System.Security.Cryptography; 

namespace CCVolunteerScheduler.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/
        VolunteersDBEntities _db = new VolunteersDBEntities();
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Volunteer user)
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();

            //if (EmailValidation(user.Email), PasswordValidation(user.Password))
            //ComputeHash code here to check with the database
            var salt = new Byte[16];
            string hashedPassword = HashPassword(user.Password);
            Volunteer currentUser = _db.Volunteers.FirstOrDefault(v => v.Email == user.Email);
            long currentUserId = 0;
            if (currentUser != null)
            {
                currentUserId = currentUser.ID;
            }
            string userId = _db.ValidateUser(currentUserId, hashedPassword).FirstOrDefault();

            string message = string.Empty;

            if (userId == "false")
            {
                message = "Username and/or password is incorrect.";
            }
            else
            {
                FormsAuthentication.SetAuthCookie(user.Email, true);
                string adminUser = _db.Check_Admin((int)currentUserId).FirstOrDefault();

                if (adminUser == "true")
                    return RedirectToAction("AdminHome", "Home", new { id = currentUserId });
                else
                    return RedirectToAction("MySchedule", "Home", new { id = currentUserId });
            }

            ViewBag.Message = message;
            return View(user);
        }
        public string HashPassword(string Password)
        {
            //you can change salt
            string salt = "Salt87978adfadfHJHU";
            Password += salt;
            SHA256CryptoServiceProvider hashProvider = new SHA256CryptoServiceProvider();
            byte[] bytePassword = System.Text.Encoding.ASCII.GetBytes(Password);
            byte[] hashBytePassword = hashProvider.ComputeHash(bytePassword);
            var hashedPassword = BitConverter.ToString(hashBytePassword);

            return hashedPassword;
        }

        public ActionResult PDYfkXYuBGyPhWgzDhBQsRnUkQjZKAtC()
        {
            DateTime today = DateTime.Now;

            //Instantiate and create a new email message
            SmtpClient client = new SmtpClient();
            MailMessage autoReminder = new MailMessage();
            //autoReminder.From = new MailAddress("challengedchampions@yahoo.com");
            autoReminder.Subject = "Automatic Reminder: Your event today at Challenged Champions Equestrian Center";
            autoReminder.Body = "This is an automated message reminding you that you've signed up for an event today. Please check the \"My Schedule\" portion of the volunteer website for more information about this event.";
            autoReminder.To.Add("frickc@findlay.edu");

            VolunteersDBEntities _db = new VolunteersDBEntities();

            foreach (string email in _db.TodaysVolunteers(today.Date))
            {
                autoReminder.Bcc.Add(email);
            }

            if(autoReminder.To.Count != 0)
                client.Send(autoReminder);
            else
                return RedirectToAction("Login", "Login");

            return RedirectToAction("Login", "Login");
        }
    }
}
