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
        private readonly IAmoCrmImportManager importManager;
        private readonly EFContextProvider<AttendanceDbContext> contextProvider = new EFContextProvider<AttendanceDbContext>();

        public BreezeController(IAmoCrmImportManager importManager)
        {
            this.importManager = importManager;
        }

        [HttpGet]
        public string Metadata()
        {
            return contextProvider.Metadata();
        }

        [HttpGet]
        public object Lookups()
        {
            return new
            {
                levels = contextProvider.Context.Levels
            };
        }

        [HttpGet]
        public IQueryable<Lead> Leads()
        {
            var dbQuery = contextProvider.Context.Leads.Include("LanguageLevel").Include("Status").Include("Lessons").Include("ResponsibleUser");

            return dbQuery;
        }

        [HttpGet]
        public IQueryable<Contact> Contacts()
        {
            return contextProvider.Context.Contacts;
        }

        [HttpGet]
        public IQueryable<Contact> ContactsOfLead(int leadId)
        {
            var lead = contextProvider.Context.Leads.FirstOrDefault(l => l.Id == leadId);
            var a = lead.Contacts.ToList();
            var b = a.ToArray();
            
            var contactsOfLead = contextProvider.Context.Contacts.Where(c => c.Leads.Select(l => l.Id).Contains(leadId));
            var cc = contactsOfLead.ToArray();
            
            return contactsOfLead;

            //var lead = contextProvider.Context.Leads.Include("Contacts").FirstOrDefault(l => l.Id == leadId);
            //if (lead == null)
            //{
            //    return null;
            //}

            //return lead.Contacts.AsQueryable();
        }

        [HttpGet]
        public IQueryable<User> Users()
        {
            return contextProvider.Context.Users;
        }

        [HttpGet]
        public IQueryable<Lesson> Lessons()
        {
            return contextProvider.Context.Lessons.Include("Lead").Include("Attendances");
        }

        [HttpPost]
        public SaveResult SaveChanges(JObject saveBundle)
        {
            return contextProvider.SaveChanges(saveBundle);
        }

        [HttpGet]
        public object Import([FromUri] AmoCrmImportOptions options)
        {
            var amoCrmImportResult = importManager.Import(options ?? new AmoCrmImportOptions());

            return EntitiesCountResult();
        }

        [HttpGet]
        public object Clear()
        {
            importManager.ClearExistingAttendanceData();

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
                NumberOfLeads = contextProvider.Context.Leads.Count(),
                NumberOfContacts = contextProvider.Context.Contacts.Count(),
                NumberOfUsers = contextProvider.Context.Users.Count(),
                NumberOfLevels = contextProvider.Context.Levels.Count(),
                NumberOfLessons = contextProvider.Context.Lessons.Count(),
                NumberOfAttendances = contextProvider.Context.Attendances.Count()
            };
        }
    }
}