using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    public sealed class ApiCustomFieldsRootResponse
    {
        [JsonProperty("contacts")]
        public ApiCustomFieldDefinitionResponse[] ContactFields { get; set; }

        [JsonProperty("leads")]
        public ApiCustomFieldDefinitionResponse[] LeadFields { get; set; }
    }
}