using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM
{
    [UsedImplicitly]
    public sealed class ApiLeadResponse : ApiNamedBusinessEntityResponseBase
    {
        [JsonProperty("price")]
        public double Price { get; set; }
    }
}