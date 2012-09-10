using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using Domain.Concrete;
using Domain.Entities;

namespace callCRM.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public ActionResult Settings()
        {
            return View();
        }
        [Authorize]
        public ActionResult CheckForNewEvents(string user)
        {
            //TODO poprawic to, zeby dostawac repository przez Ninject
            //TODO poprawic w ten sposob, zeby odpytywac jednociesnie mogla tylko jedna strona (kartka) dla jednego usera
            if (!string.IsNullOrEmpty(user))
            {
                var repository = new EFEventRepository();
                var currentUser = Membership.GetUser(user);
                var employee = repository.GetEmployee((Guid)currentUser.ProviderUserKey);
                if (employee != null)
                {
                    //TODO wyniesc statusy do odzielnej klasy, lub helpera, lub zrobic konstanty
                    var newEvent = repository.SearchFor(e => e.EmployeeID == employee.EmployeeID && e.IsServedBy == false && e.EventStatusID == EventStatus.ID_STATUS_RINGING).FirstOrDefault();
                    if (newEvent != null)
                        return Json(new { state = "RINGING", client = newEvent.Client.FirstName + newEvent.Client.LastName, number = newEvent.Client.PhoneNumber, company = newEvent.Client.Company.Name, stage = newEvent.Client.ContactStage.Name, client_id = newEvent.Client.ClientID, event_id = newEvent.EventID, inbound = newEvent.Inbound });
                    else
                        return Json(new { state = "NOEVENTS", info = "" });
                }
            }
            return null;
        }

        [Authorize]
        public ActionResult CallTo(string number)
        {
            var currentUser = Membership.GetUser(User.Identity.Name);
            var employee = new EFEventRepository().GetEmployee((Guid)currentUser.ProviderUserKey);

            var path = Request.PhysicalApplicationPath;

            if (employee == null)
                throw new Exception("No user found!");

            string asteriskOutAppPath = path + "bin\\Asterisk.Outgoing.exe";

            string protocol = "SIP";
            string context = "phones";
            string callerId = "<1001>";

            string asteriskOutArguments = protocol + "/" + employee.AsteriskName + " " + callerId + " " + number + " " + context;

            //Executing asteriks out application with parametres
            System.Diagnostics.Process.Start(@asteriskOutAppPath, asteriskOutArguments);

            return Json(new { result = "ok" });
        }
    }
}
