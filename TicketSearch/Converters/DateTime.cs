using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace TicketSearch.Converters.DateTime
{
    class Converter : JsonConverter<System.DateTime>
    {
        private const string _customFormat = "yyyy-MM-dd'T'HH:mm:ss K";
        public override System.DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            System.DateTime.ParseExact(reader.GetString(), _customFormat, CultureInfo.InvariantCulture);
        public override void Write(
            Utf8JsonWriter writer,
            System.DateTime value,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString(_customFormat, CultureInfo.InvariantCulture));
    }
    class ConverterNullable : JsonConverter<System.DateTime?>
    {
        private const string _customFormat = "yyyy-MM-dd'T'HH:mm:ss K";
        public override System.DateTime? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            System.DateTime.ParseExact(reader.GetString(), _customFormat, CultureInfo.InvariantCulture);
        public override void Write(
            Utf8JsonWriter writer,
            System.DateTime? value,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}