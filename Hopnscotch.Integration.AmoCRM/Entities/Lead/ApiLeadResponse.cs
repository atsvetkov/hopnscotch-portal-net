using System;
using System.Globalization;
using System.Linq;
using Hopnscotch.Integration.AmoCRM.Annotations;
using Newtonsoft.Json;

namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    [UsedImplicitly]
    public sealed class ApiLeadResponse : ApiNamedBusinessEntityWithFieldsResponseBase
    {
        private const string ScheduleCustomFieldName = "Расписание";
        private const string StartDateCustomFieldName = "Старт группы";

        private const string AmoDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

        [JsonProperty("price")]
        public double? Price { get; set; }

        public string ScheduleText
        {
            get
            {
                var customField = FindCustomField(ScheduleCustomFieldName);
                if (customField == null || customField.Values == null || !customField.Values.Any())
                {
                    return string.Empty;
                }

                return customField.Values[0].Value;
            }
        }

        public DateTime? StartDate
        {
            get
            {
                var customField = FindCustomField(StartDateCustomFieldName);
                if (customField == null || customField.Values == null || !customField.Values.Any())
                {
                    return null;
                }

                DateTime startDate;
                if (!DateTime.TryParseExact(customField.Values[0].Value, AmoDateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out startDate))
                {
                    return null;
                }

                return startDate;
            }
        }
    }
}