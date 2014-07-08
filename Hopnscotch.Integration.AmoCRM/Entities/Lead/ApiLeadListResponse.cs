using Hopnscotch.Integration.AmoCRM;
using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    [AmoCrmResponseType("Leads")]
    public sealed class ApiLeadListResponse : ApiListResponseBase<ApiLeadResponse>
    {
        [JsonProperty("leads")]
        public override ApiLeadResponse[] Entities { get; set; }
    }
}