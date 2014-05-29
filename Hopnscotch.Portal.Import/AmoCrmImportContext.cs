using System;
using System.Collections.Generic;
using System.Linq;
using Hopnscotch.Portal.Integration.AmoCRM.DataProvider;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Import
{
    internal sealed class AmoCrmImportContext
    {
        private const string LevelCustomFieldName = "Уровень";
        
        private readonly string[] actualLeadStatusList = new[]
        {
            "Учится"
        };
        
        public IDictionary<int, Contact> ContactsMap { get; private set; }
        public IDictionary<int, Lead> LeadsMap { get; private set; }
        public IDictionary<int, User> UsersMap { get; private set; }
        public IDictionary<int, Level> LevelsMap { get; private set; }
        public IDictionary<int, LeadStatus> LeadStatusMap { get; private set; }
        public ApiContactLeadLinkResponse[] ContactLeadLinks { get; private set; }

        public AmoCrmImportContext(IAmoDataProvider provider, IAmoCrmEntityConverter converter, bool excludeHistoricalData = true)
        {
            var contactsResponse = provider.GetContacts();
            var leadsResponse = provider.GetLeads();
            var contactLeadLinksResponse = provider.GetContactLeadLinks();
            var accountResponse = provider.GetAccount();

            ContactsMap = contactsResponse.Response.Contacts.Select(converter.Convert).ToDictionary(c => c.AmoId);
            var leads = leadsResponse.Response.Leads.Select(converter.Convert);
            UsersMap = accountResponse.Response.Account.Users.Select(converter.Convert).ToDictionary(c => c.AmoId);
            ContactLeadLinks = contactLeadLinksResponse.Response.Links;

            SetupLevels(converter, accountResponse);
            SetupLeadStatuses(converter, accountResponse);

            if (excludeHistoricalData)
            {
                leads = leads.Where(l => !IsHistorical(l));
            }

            LeadsMap = leads.ToDictionary(c => c.AmoId);
        }

        private bool IsHistorical(Lead lead)
        {
            if (lead == null)
            {
                throw new ArgumentNullException("lead");
            }

            if (lead.EndDate.HasValue && lead.EndDate.Value < DateTime.Today)
            {
                return true;
            }

            LeadStatus status;
            if (LeadStatusMap.TryGetValue(lead.AmoStatusId, out status) && !actualLeadStatusList.Contains(status.Name))
            {
                return true;
            }
            
            return false;
        }

        private void SetupLeadStatuses(IAmoCrmEntityConverter converter, ApiResponseRoot<ApiAccountRootResponse> accountResponse)
        {
            LeadStatusMap = accountResponse.Response.Account.LeadStatuses.Select(converter.Convert).ToDictionary(c => c.AmoId);
        }

        private void SetupLevels(IAmoCrmEntityConverter converter, ApiResponseRoot<ApiAccountRootResponse> accountResponse)
        {
            var levelsCustomField = accountResponse.Response.Account.CustomFields.LeadFields.FirstOrDefault(f => f.Name == LevelCustomFieldName);
            LevelsMap = new Dictionary<int, Level>();
            if (levelsCustomField != null)
            {
                LevelsMap = converter.Convert<Level>(levelsCustomField).ToDictionary(l => l.AmoId);
            }
        }
    }
}