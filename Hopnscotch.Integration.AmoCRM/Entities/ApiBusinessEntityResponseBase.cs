using System;
using Hopnscotch.Integration.AmoCRM.Converters;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    public abstract class ApiBusinessEntityResponseBase : ApiEntityResponseBase
    {
        [JsonProperty("date_create")]
        [JsonConverter(typeof(CustomJsonDateConverter))]
        public DateTime Created { get; set; }

        [JsonProperty("last_modified")]
        [JsonConverter(typeof(CustomJsonDateConverter))]
        public DateTime LastModified { get; set; }

        [JsonProperty("responsible_user_id")]
        public string ResponsibleUserId { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }
    }
}