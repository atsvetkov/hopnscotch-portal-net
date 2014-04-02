using System.Linq;
using System.Web.Http;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Import;

namespace Hopnscotch.Portal.Web.Controllers
{
    public class AdminController : ApiControllerBase
    {
        private readonly IAmoCrmImportManager _importManager;

        public AdminController(IAttendanceUow attendanceUow, IAmoCrmImportManager importManager)
            : base(attendanceUow)
        {
            _importManager = importManager;
        }

        [HttpPost]
        [Route("api/admin/import")]
        public TotalsResult Import()
        {
            var amoCrmImportResult = _importManager.Import(new AmoCrmImportOptions());

            return new TotalsResult
            {
                NumberOfLeads = _attendanceUow.Leads.GetAll().Count(),
                NumberOfContacts = _attendanceUow.Contacts.GetAll().Count(),
                NumberOfUsers = _attendanceUow.Users.GetAll().Count()
            };
        }

        [HttpPost]
        [Route("api/admin/refresh")]
        public TotalsResult Refresh()
        {
            return new TotalsResult
            {
                NumberOfLeads = _attendanceUow.Leads.GetAll().Count(),
                NumberOfContacts = _attendanceUow.Contacts.GetAll().Count(),
                NumberOfUsers = _attendanceUow.Users.GetAll().Count()
            };
        }
    }

    public sealed class TotalsResult
    {
        public int NumberOfLeads { get; set; }
        public int NumberOfContacts { get; set; }
        public int NumberOfUsers { get; set; }
    }
}