using Hopnscotch.Integration.AmoCRM;
using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    [AmoCrmResponseType("Leads")]
    public sealed class ApiLeadListResponse
    {
        [JsonProperty("leads")]
        public ApiLeadResponse[] Leads { get; set; }
    }
}