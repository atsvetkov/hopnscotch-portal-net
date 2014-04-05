using System.Collections.Generic;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    public class ApiCustomFieldDefinitionResponse : ApiEntityResponseBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("multiple")]
        public string Multiple { get; set; }

        [JsonProperty("type_id")]
        public int TypeId { get; set; }

        [JsonProperty("enums")]
        public Dictionary<int, string> Enums { get; set; }
    }
}