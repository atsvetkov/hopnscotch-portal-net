using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    public abstract class ApiEntityResponseBase
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}