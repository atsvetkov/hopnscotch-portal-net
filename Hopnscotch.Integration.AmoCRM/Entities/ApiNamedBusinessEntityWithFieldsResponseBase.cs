using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    public abstract class ApiNamedBusinessEntityWithFieldsResponseBase : ApiNamedBusinessEntityResponseBase
    {
        protected IDictionary<string, ApiCustomFieldResponse> customFieldsMap;

        [JsonProperty("custom_fields")]
        public ApiCustomFieldResponse[] CustomFields { get; set; }

        protected ApiCustomFieldResponse FindCustomField(string customFieldName)
        {
            if (CustomFields == null)
            {
                return null;
            }

            if (customFieldsMap == null)
            {
                customFieldsMap = CustomFields.ToDictionary(f => f.Name);
            }

            ApiCustomFieldResponse field;
            if (!customFieldsMap.TryGetValue(customFieldName, out field))
            {
                return null;
            }

            return field;
        }
    }
}