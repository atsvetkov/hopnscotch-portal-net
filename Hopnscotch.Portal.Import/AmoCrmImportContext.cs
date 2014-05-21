using System.Collections.Generic;
using System.Linq;
using Hopnscotch.Portal.Integration.AmoCRM.DataProvider;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Import
{
    public sealed class AmoCrmImportContext
    {
        private const string LevelCustomFieldName = "Уровень";

        public IDictionary<int, Contact> ContactsMap { get; private set; }
        public IDictionary<int, Lead> LeadsMap { get; private set; }
        public IDictionary<int, User> UsersMap { get; private set; }
        public IDictionary<int, Level> LevelsMap { get; private set; }
        public ApiContactLeadLinkResponse[] ContactLeadLinks { get; private set; }

        public AmoCrmImportContext(IAmoDataProvider provider, IAmoCrmEntityConverter converter)
        {
            var contactsResponse = provider.GetContacts();
            var leadsResponse = provider.GetLeads();
            var contactLeadLinksResponse = provider.GetContactLeadLinks();
            var accountResponse = provider.GetAccount();

            ContactsMap = contactsResponse.Response.Contacts.Select(converter.Convert).ToDictionary(c => c.AmoId);
            LeadsMap = leadsResponse.Response.Leads.Select(converter.Convert).ToDictionary(c => c.AmoId);
            UsersMap = accountResponse.Response.Account.Users.Select(converter.Convert).ToDictionary(c => c.AmoId);
            ContactLeadLinks = contactLeadLinksResponse.Response.Links;

            var levelsCustomField = accountResponse.Response.Account.CustomFields.LeadFields.FirstOrDefault(f => f.Name == LevelCustomFieldName);
            LevelsMap = new Dictionary<int, Level>();
            if (levelsCustomField != null)
            {
                LevelsMap = converter.Convert<Level>(levelsCustomField).ToDictionary(l => l.AmoId);
            }
        }
    }
}