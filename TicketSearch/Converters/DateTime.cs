using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace TicketSearch.Converters.DateTime
{
    class Converter : JsonConverter<System.DateTime>
    {
        private const string CustomFormat = "yyyy-MM-dd'T'HH:mm:ss K";
        public override System.DateTime Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            System.DateTime.ParseExact(reader.GetString(), CustomFormat, CultureInfo.InvariantCulture);
        public override void Write(
            Utf8JsonWriter writer,
            System.DateTime value,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString(CustomFormat, CultureInfo.InvariantCulture));
    }
    class ConverterNullable : JsonConverter<System.DateTime?>
    {
        private const string CustomFormat = "yyyy-MM-dd'T'HH:mm:ss K";
        public override System.DateTime? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
            System.DateTime.ParseExact(reader.GetString(), CustomFormat, CultureInfo.InvariantCulture);
        public override void Write(
            Utf8JsonWriter writer,
            System.DateTime? value,
            JsonSerializerOptions options) =>
            writer.WriteStringValue(value.ToString());
    }
}