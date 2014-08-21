using System.Linq;
using System.Web.Http;
using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using Hopnscotch.Portal.Data;
using Hopnscotch.Portal.Import;
using Hopnscotch.Portal.Model;
using Newtonsoft.Json.Linq;

namespace Hopnscotch.Portal.Web.Controllers
{
    [BreezeController]
    public class BreezeController : ApiController
    {
        private readonly IAmoCrmImportManager _importManager;
        private readonly CustomEFContextProvider<AttendanceDbContext> _contextProvider;

        public BreezeController(IAmoCrmImportManager importManager, IAttendanceDbContextFactory attendanceDbContextFactory)
        {
            _importManager = importManager;
            _contextProvider = new CustomEFContextProvider<AttendanceDbContext>(attendanceDbContextFactory);
        }

        [HttpGet]
        public string Metadata()
        {
            return _contextProvider.Metadata();
        }

        [HttpGet]
        public object Lookups()
        {
            return new
            {
                levels = _contextProvider.Context.Levels
            };
        }

        [HttpGet]
        public IQueryable<Lead> Leads()
        {
            var dbQuery = _contextProvider.Context.Leads.Include("LanguageLevel").Include("Status").Include("Lessons").Include("ResponsibleUser");

            return dbQuery;
        }

        [HttpGet]
        public IQueryable<Contact> Contacts()
        {
            return _contextProvider.Context.Contacts;
        }

        [HttpGet]
        public IQueryable<Contact> ContactsOfLead(int leadId)
        {
            var contactsOfLead = _contextProvider.Context.Contacts.Where(c => c.Leads.Select(l => l.Id).Contains(leadId));
            
            return contactsOfLead;
        }

        [HttpGet]
        public IQueryable<User> Users()
        {
            return _contextProvider.Context.Users;
        }

        [HttpGet]
        public IQueryable<Lesson> Lessons()
        {
            return _contextProvider.Context.Lessons.Include("Lead").Include("Attendances");
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return _contextProvider.SaveChanges(saveBundle);
        }

        [HttpGet]
        public object Import([FromUri] AmoCrmImportOptions options)
        {
            var amoCrmImportResult = _importManager.Import(options ?? new AmoCrmImportOptions());

            return EntitiesCountResult();
        }

        [HttpGet]
        public object Clear()
        {
            _importManager.ClearExistingAttendanceData();

            return EntitiesCountResult();
        }

        [HttpGet]
        public object Refresh()
        {
            return EntitiesCountResult();
        }

        private object EntitiesCountResult()
        {
            return new
            {
                NumberOfLeads = _contextProvider.Context.Leads.Count(),
                NumberOfContacts = _contextProvider.Context.Contacts.Count(),
                NumberOfUsers = _contextProvider.Context.Users.Count(),
                NumberOfLevels = _contextProvider.Context.Levels.Count(),
                NumberOfLessons = _contextProvider.Context.Lessons.Count(),
                NumberOfAttendances = _contextProvider.Context.Attendances.Count()
            };
        }
    }
}