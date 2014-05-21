using Hopnscotch.Integration.AmoCRM;
using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    [AmoCrmResponseType("Tasks")]
    public sealed class ApiTaskListResponse
    {
        [JsonProperty("tasks")]
        public ApiTaskResponse[] Tasks { get; set; }
    }
}