using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    public abstract class ApiNamedBusinessEntityWithFieldsResponseBase : ApiBusinessEntityResponseBase
    {
        [JsonProperty("custom_fields")]
        public ApiCustomFieldResponse[] CustomFields { get; set; }
    }
}