using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiContactListResponse
    {
        [JsonProperty("contacts")]
        public ApiIndividualContactResponse[] Contacts { get; set; }
    }
}