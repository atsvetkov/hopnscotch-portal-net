using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Hopnscotch.Integration.AmoCRM.Converters
{
    internal sealed class CustomJsonDateConverter : DateTimeConverterBase
    {
        private readonly DateTime _epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteRawValue(((DateTime)value - _epoch).TotalMilliseconds + "000");
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return _epoch.AddSeconds((long)reader.Value);
        }
    }
}
