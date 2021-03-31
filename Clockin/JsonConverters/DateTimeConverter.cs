using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Clockin.JsonConverters
{
    [ExcludeFromCodeCoverage]
    public class DateTimeConverter: JsonConverter<DateTime>
    {
        private const string DateTimeFormatString = "yyyy/MM/dd";
        
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.ParseExact(reader.GetString()!, DateTimeFormatString, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, DateTime dateTimeValue, JsonSerializerOptions options)
        {
            writer.WriteStringValue(dateTimeValue.ToString(DateTimeFormatString, CultureInfo.InvariantCulture));
        }
    }
}