using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    public sealed class ApiLeadStatusResponse : ApiEntityResponseBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("editable")]
        public string Editable { get; set; }
    }
}