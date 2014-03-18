using Hopnscotch.Integration.AmoCRM.Entities;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    public abstract class ApiNamedBusinessEntityWithFieldsResponseBase : ApiNamedBusinessEntityResponseBase
    {
        [JsonProperty("custom_fields")]
        public ApiCustomFieldResponse[] CustomFields { get; set; }
    }
}