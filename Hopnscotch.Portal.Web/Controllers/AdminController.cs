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
    }
}