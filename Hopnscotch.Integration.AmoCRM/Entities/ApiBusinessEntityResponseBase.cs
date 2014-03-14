using System;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM
{
    public abstract class ApiBusinessEntityResponseBase : ApiEntityResponseBase
    {
        [JsonProperty("date_create")]
        public DateTime Created { get; set; }

        [JsonProperty("last_modified")]
        public string LastModified { get; set; }

        [JsonProperty("responsible_user_id")]
        public string ResponsibleUserId { get; set; }

        [JsonProperty("account_id")]
        public string AccountId { get; set; }
    }
}