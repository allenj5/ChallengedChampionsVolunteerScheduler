using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CCVolunteerScheduler.Models;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;

namespace CCVolunteerScheduler.Controllers
{
    public class TextController : TwilioController
    {
        public ActionResult SendSMS(string[] phoneNumbers, string textMessage)
        {
            var accountsid = "AC1e234cf4a6742315d15d6d0f457c3d55";
            var authToken = "8f0c777ebd152c1145b6c90f82d29ac4";
            TwilioClient.Init(accountsid, authToken);

            var from = new PhoneNumber("+14193703444");

            foreach (var number in phoneNumbers)
            {
                var message = MessageResource.Create(
                    to: "+1" + number,
                    from: from,
                    body: textMessage + "    (Please do not respond to this phone number. Instead, please contact 4192350626. Thanks!)"
                    );
            }

            return RedirectToAction("Communications", "Home");
        }


        public ActionResult TextTodaysVolunteers()
        {
            DateTime today = DateTime.Now;

            VolunteersDBEntities _db = new VolunteersDBEntities();

            var accountsid = "AC1e234cf4a6742315d15d6d0f457c3d55";
            var authToken = "8f0c777ebd152c1145b6c90f82d29ac4";
            TwilioClient.Init(accountsid, authToken);

            var from = new PhoneNumber("+14193703444");

            foreach (string email in _db.TodaysVolunteersPhone(today.Date))
            {
                var message = MessageResource.Create(
                   to: "+1" + email,
                   from: from,
                   body: "This is an automated message from Challenged Champions Equestrian Center. Reminder: you are signed up for an event today!"
                   );
            }

            return RedirectToAction("Communications", "Home");
        }

    }
}
