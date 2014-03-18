using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    public abstract class ApiEntityResponseBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}