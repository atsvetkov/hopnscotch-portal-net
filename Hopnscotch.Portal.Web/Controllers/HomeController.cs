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
        public ActionResult Index(IAmoCrmImportManager importManager, IAttendanceUow attendanceUow)
        {
            var amoCrmImportResult = importManager.Import(new AmoCrmImportOptions());

            var contacts = attendanceUow.Contacts.GetAll().ToArray();
            var leads = attendanceUow.Leads.GetAll().ToArray();

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
