using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hopnscotch.Portal.Contracts;

namespace Hopnscotch.Portal.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(IAttendanceUow uow)
        {
            var lessons = uow.Lessons.GetAll().ToArray();

            ViewBag.Title = "Home Page";

            return View();
        }
    }
}
