using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiLeadListResponse
    {
        [JsonProperty("leads")]
        public ApiLeadResponse[] Leads { get; set; }
    }
}