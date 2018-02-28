﻿using System;
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

    }
}
