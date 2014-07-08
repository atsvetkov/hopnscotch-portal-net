using Hopnscotch.Integration.AmoCRM.Annotations;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    [AmoCrmResponseType("ContactLeadLinks")]
    public sealed class ApiContactLeadLinkListResponse : ApiListResponseBase<ApiContactLeadLinkResponse>
    {
        [JsonProperty("links")]
        public override ApiContactLeadLinkResponse[] Entities { get; set; }
    }
}