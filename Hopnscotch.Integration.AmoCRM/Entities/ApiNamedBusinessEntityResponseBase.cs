using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    public abstract class ApiNamedBusinessEntityResponseBase : ApiBusinessEntityResponseBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}