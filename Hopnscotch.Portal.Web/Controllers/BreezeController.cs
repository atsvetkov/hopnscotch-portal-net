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
            return contextProvider.Context.Leads.Include("LanguageLevel");
        }

        [HttpGet]
        public IQueryable<Contact> Contacts()
        {
            return contextProvider.Context.Contacts;
        }

        [HttpGet]
        public IQueryable<User> Users()
        {
            return contextProvider.Context.Users;
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