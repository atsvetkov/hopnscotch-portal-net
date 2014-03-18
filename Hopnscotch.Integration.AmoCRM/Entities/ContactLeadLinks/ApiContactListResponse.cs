using Hopnscotch.Integration.AmoCRM.Annotations;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiContactLeadLinkListResponse
    {
        [JsonProperty("links")]
        public ApiContactLeadLinkResponse[] Links { get; set; }
    }
}