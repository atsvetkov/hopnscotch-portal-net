using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Import;

namespace Hopnscotch.Portal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(IAttendanceUow attendanceUow)
        {
            var contacts = attendanceUow.Contacts.GetAll(c => c.Leads).ToArray();
            var leads = attendanceUow.Leads.GetAll(l => l.Contacts).ToArray();
            var leadsWithSomeContacts = attendanceUow.Leads.GetAll().Where(l => l.Contacts.Count > 0).ToArray();

            var cc = leadsWithSomeContacts[0].Contacts.ToArray();

            var contactsWithSomeLeads = attendanceUow.Contacts.GetAll().Where(c => c.Leads.Count > 0).ToArray();

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
