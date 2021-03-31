using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Clockin.JsonConverters
{
    [ExcludeFromCodeCoverage]
    public class TimespanConverter : JsonConverter<TimeSpan>
    {
        private const string TimeSpanFormatString = @"hh\:mm";

        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            TimeSpan.TryParseExact(reader.GetString(), TimeSpanFormatString, null, out var parsedTimeSpan);
            return parsedTimeSpan;
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            var timespanFormatted = $"{value.ToString(TimeSpanFormatString)}";
            writer.WriteStringValue(timespanFormatted);
        }
    }
}