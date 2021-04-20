using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using System;
using System.Data;
namespace TicketSearch.Model
{

    public class Organization
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("domain_names")]
        public List<string> DomainNames { get; set; }
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        [JsonPropertyName("created_at")]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("shared_tickets")]
        public bool SharedTickets { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }
        public static List<Organization> Get(string data)
        {
            return JsonSerializer.Deserialize<List<Organization>>(data, new JsonSerializerOptions { IgnoreNullValues = true });
        }
        public Organization Map(DataRow row)
        {
            Id = row.Field<int>("Id");
            Url = row.Field<string>("Url");
            ExternalId = row.Field<string>("ExternalId");
            Name = row.Field<string>("Name");
            DomainNames = row.Field<List<string>>("DomainNames");
            CreatedAt = row.Field<DateTime>("CreatedAt");
            Details = row.Field<string>("Details");
            SharedTickets = row.Field<bool>("SharedTickets");
            Tags = row.Field<List<string>>("Tags");

            return this;
        }
    }

}