using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Integration.AmoCRM.DataProvider;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Import
{
    public sealed class AmoCrmImportManager : IAmoCrmImportManager
    {
        private readonly IAmoDataProvider _amoDataProvider;
        private readonly IAttendanceUow _attendanceUow;
        private readonly IAmoCrmEntityConverter _entityConverter;

        public AmoCrmImportManager(IAmoDataProvider amoDataProvider, IAttendanceUow attendanceUow, IAmoCrmEntityConverter entityConverter)
        {
            _amoDataProvider = amoDataProvider;
            _attendanceUow = attendanceUow;
            _entityConverter = entityConverter;
        }

        public AmoCrmImportResult Import(AmoCrmImportOptions options)
        {
            if (!_amoDataProvider.AuthenticateAsync().Result)
            {
                return new AmoCrmImportResult(new []
                {
                    new AmoCrmImportResultError
                    {
                        EntityType = AmoCrmEntityTypes.None,
                        Message = "Could not authenticate in amoCRM API"
                    }
                });
            }

            var contactsResponse = _amoDataProvider.GetContactsAsync().Result;
            var leadsResponse = _amoDataProvider.GetLeadsAsync().Result;
            var contactLeadLinksResponse = _amoDataProvider.GetContactLeadLinksAsync().Result;
            
            var contactsMap = contactsResponse.Response.Contacts.Select(r => _entityConverter.Convert(r)).ToDictionary(c => c.AmoId);
            var leadsMap = leadsResponse.Response.Leads.Select(r => _entityConverter.Convert(r)).ToDictionary(c => c.AmoId);
            var contactLeadLinks = contactLeadLinksResponse.Response.Links;
            
            foreach (var link in contactLeadLinks)
            {
                Contact contact;
                Lead lead;
                if (contactsMap.TryGetValue(link.ContactId, out contact) && leadsMap.TryGetValue(link.LeadId, out lead))
                {
                    contact.Leads.Add(lead);
                }
            }

            foreach (var contact in contactsMap.Values)
            {
                _attendanceUow.Contacts.Add(contact);
            }

            foreach (var lead in leadsMap.Values)
            {
                _attendanceUow.Leads.Add(lead);
            }
            
            _attendanceUow.Commit();

            return new AmoCrmImportResult();
        }

        public void Dispose()
        {
            _attendanceUow.Dispose();
        }
    }
}
