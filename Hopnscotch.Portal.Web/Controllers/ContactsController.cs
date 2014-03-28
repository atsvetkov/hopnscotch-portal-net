using System.Collections.Generic;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Web.Controllers
{
    public class ContactsController : ApiControllerBase
    {
        public ContactsController(IAttendanceUow attendanceUow) : base(attendanceUow)
        {
        }

        public IEnumerable<Contact> Get()
        {
            return _attendanceUow.Contacts.GetAll();
        }
    }
}