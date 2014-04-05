using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiAccountResponse : ApiEntityResponseBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("subdomain")]
        public string Subdomain { get; set; }

        [JsonProperty("users")]
        public ApiUserResponse[] Users { get; set; }

        [JsonProperty("leads_statuses")]
        public ApiLeadStatusResponse[] LeadStatuses { get; set; }

        [JsonProperty("custom_fields")]
        public ApiCustomFieldsRootResponse CustomFields { get; set; }
    }
}