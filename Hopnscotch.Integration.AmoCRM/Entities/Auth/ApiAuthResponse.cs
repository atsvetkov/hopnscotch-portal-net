using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiAuthResponse
    {
        [JsonProperty("auth")]
        public bool IsAuthorized { get; set; }
    }
}