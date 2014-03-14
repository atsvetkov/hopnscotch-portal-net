using System;
using Hopnscotch.Integration.AmoCRM.Annotations;
using Hopnscotch.Integration.AmoCRM.Converters;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiTaskResponse : ApiNamedBusinessEntityResponseBase
    {
        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("task_type")]
        public string TaskType { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("complete_till")]
        [JsonConverter(typeof(CustomJsonDateConverter))]
        public DateTime CompleteUntil { get; set; }
    }
}