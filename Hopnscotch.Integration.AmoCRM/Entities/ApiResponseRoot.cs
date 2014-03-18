using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiResponseRoot<T> where T : class
    {
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}