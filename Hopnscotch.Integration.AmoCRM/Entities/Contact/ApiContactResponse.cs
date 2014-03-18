using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Integration.AmoCRM.Entities
{
    public abstract class ApiBaseContactResponse : ApiNamedBusinessEntityWithFieldsResponseBase
    {
        [JsonProperty("type")]
        public string ContactType { get; set; }
    }

    [UsedImplicitly]
    public sealed class ApiIndividualContactResponse : ApiBaseContactResponse
    {
    }

    [UsedImplicitly]
    public sealed class ApiCompanyContactResponse : ApiBaseContactResponse
    {
    }
}