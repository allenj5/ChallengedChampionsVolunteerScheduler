using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCVolunteerScheduler.Models;
using System.Web.Security;

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
        public ActionResult Login(User user)
        {
            VolunteersDBEntities _db = new VolunteersDBEntities();
            string userId = _db.ValidateUser(user.Username, user.Password).FirstOrDefault();

            string message = string.Empty;

            if(userId == "false")
            {
                message = "Username and/or password is incorrect.";
            }
            else
            {
                return RedirectToAction("Home");
            }

            ViewBag.Message = message;
            return View(user);
        }
    }
}
