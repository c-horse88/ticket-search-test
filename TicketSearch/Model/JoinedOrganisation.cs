using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using System;
using System.Data;
namespace TicketSearch.Model
{
    public class JoinedOrganisation : Organization
    {
        [JsonPropertyName("_id")]
        public new int Id { get; set; }

        [JsonPropertyName("url")]
        public new string Url { get; set; }

        [JsonPropertyName("external_id")]
        public new string ExternalId { get; set; }

        [JsonPropertyName("name")]
        public new string Name { get; set; }

        [JsonPropertyName("domain_names")]
        public new List<string> DomainNames { get; set; }
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        [JsonPropertyName("created_at")]
        public new DateTime? CreatedAt { get; set; }

        [JsonPropertyName("details")]
        public new string Details { get; set; }

        [JsonPropertyName("shared_tickets")]
        public new bool SharedTickets { get; set; }

        [JsonPropertyName("tags")]
        public new List<string> Tags { get; set; }
        public List<User> Users { get; set; }
        public List<Ticket> Tickets { get; set; }
        public new JoinedOrganisation Map(DataRow row)
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