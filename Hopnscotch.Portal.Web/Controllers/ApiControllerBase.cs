using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Hopnscotch.Portal.Contracts;

namespace Hopnscotch.Portal.Web.Controllers
{
    public abstract class ApiControllerBase : ApiController
    {
        protected readonly IAttendanceUow _attendanceUow;

        protected ApiControllerBase(IAttendanceUow attendanceUow)
        {
            _attendanceUow = attendanceUow;
        }
    }
}