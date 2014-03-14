using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    public abstract class ApiEntityResponseBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}