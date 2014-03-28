using System.Collections.Generic;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Web.Controllers
{
    public class LeadsController : ApiControllerBase
    {
        public LeadsController(IAttendanceUow attendanceUow)
            : base(attendanceUow)
        {
        }

        public IEnumerable<Lead> Get()
        {
            return _attendanceUow.Leads.GetAll();
        }
    }
}