using System;
using Hopnscotch.Integration.AmoCRM.Annotations;
using Hopnscotch.Integration.AmoCRM.Converters;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiCustomFieldValueResponse : ApiEntityResponseBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("enum")]
        public string Enum { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }

        [JsonProperty("last_modified")]
        [JsonConverter(typeof(CustomJsonDateConverter))]
        public DateTime LastModified { get; set; }
    }
}