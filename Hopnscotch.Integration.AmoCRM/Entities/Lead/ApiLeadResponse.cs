using Hopnscotch.Integration.AmoCRM.Annotations;
using Hopnscotch.Portal.Integration.AmoCRM.Entities;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiLeadResponse : ApiNamedBusinessEntityWithFieldsResponseBase
    {
        [JsonProperty("price")]
        public double Price { get; set; }
    }
}