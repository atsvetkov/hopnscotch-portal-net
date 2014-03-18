using System;
using Hopnscotch.Integration.AmoCRM.Annotations;
using Hopnscotch.Portal.Integration.AmoCRM.Converters;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiContactLeadLinkResponse
    {
        [JsonProperty("contact_id")]
        public int ContactId { get; set; }

        [JsonProperty("lead_id")]
        public int LeadId { get; set; }

        [JsonProperty("last_modified")]
        [JsonConverter(typeof(CustomJsonDateConverter))]
        public DateTime LastModified { get; set; }
    }
}