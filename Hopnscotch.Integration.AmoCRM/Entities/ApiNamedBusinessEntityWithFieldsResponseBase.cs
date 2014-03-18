using Hopnscotch.Integration.AmoCRM.Entities;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    public abstract class ApiNamedBusinessEntityWithFieldsResponseBase : ApiBusinessEntityResponseBase
    {
        [JsonProperty("custom_fields")]
        public ApiCustomFieldResponse[] CustomFields { get; set; }
    }
}