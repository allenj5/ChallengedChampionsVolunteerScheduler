using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
    }
}
