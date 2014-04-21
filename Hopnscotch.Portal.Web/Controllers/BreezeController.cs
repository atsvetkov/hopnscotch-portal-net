using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using Hopnscotch.Portal.Data;
using Hopnscotch.Portal.Import;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Web.Controllers
{
    [BreezeController]
    public class BreezeController : ApiController
    {
        private readonly IAmoCrmImportManager _importManager;
        private readonly EFContextProvider<AttendanceDbContext> contextProvider = new EFContextProvider<AttendanceDbContext>();

        public BreezeController(IAmoCrmImportManager importManager)
        {
            _importManager = importManager;
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
            var dbQuery = contextProvider.Context.Leads.Include("LanguageLevel").Include("Lessons").Include("ResponsibleUser");

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
            return contextProvider.Context.Contacts.Where(c => c.Leads.Select(l => l.Id).Contains(leadId));

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
            return contextProvider.Context.Lessons;
        }

        [HttpGet]
        public object Import()
        {
            var amoCrmImportResult = _importManager.Import(new AmoCrmImportOptions());

            return new 
            {
                NumberOfLeads = contextProvider.Context.Leads.Count(),
                NumberOfContacts = contextProvider.Context.Contacts.Count(),
                NumberOfUsers = contextProvider.Context.Users.Count(),
                NumberOfLevels = contextProvider.Context.Levels.Count()
            };
        }

        [HttpGet]
        public object Refresh()
        {
            return new
            {
                NumberOfLeads = contextProvider.Context.Leads.Count(),
                NumberOfContacts = contextProvider.Context.Contacts.Count(),
                NumberOfUsers = contextProvider.Context.Users.Count(),
                NumberOfLevels = contextProvider.Context.Levels.Count()
            };
        }
    }
}