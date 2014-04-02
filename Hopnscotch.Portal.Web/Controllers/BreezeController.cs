using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using Hopnscotch.Portal.Data;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Web.Controllers
{
    [BreezeController]
    public class BreezeController : ApiController
    {
        private readonly EFContextProvider<AttendanceDbContext> contextProvider = new EFContextProvider<AttendanceDbContext>();

        [HttpGet]
        public string Metadata()
        {
            return contextProvider.Metadata();
        }

        public IQueryable<Lead> Leads()
        {
            return contextProvider.Context.Leads;
        }

        public IQueryable<Contact> Contacts()
        {
            return contextProvider.Context.Contacts;
        }
    }
}