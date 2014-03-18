using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiLeadResponse : ApiNamedBusinessEntityWithFieldsResponseBase
    {
        [JsonProperty("price")]
        public double Price { get; set; }
    }
}