using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using System.Data;
using System;
namespace TicketSearch.Model
{

    public class Ticket
    {
        [JsonPropertyName("_id")]
        public string Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("priority")]
        public string Priority { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("submitter_id")]
        public int SubmitterId { get; set; }

        [JsonPropertyName("assignee_id")]
        public int AssigneeId { get; set; }

        [JsonPropertyName("organization_id")]
        public int OrganizationId { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("has_incidents")]
        public bool HasIncidents { get; set; }

        [JsonPropertyName("due_at")]
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        public DateTime? DueAt { get; set; }

        [JsonPropertyName("via")]
        public string Via { get; set; }
        public static List<Ticket> Get(string data)
        {
            return JsonSerializer.Deserialize<List<Ticket>>(data, new JsonSerializerOptions { IgnoreNullValues = true });
        }
        public Ticket Map(DataRow row)
        {
            Id = row.Field<string>("Id");
            Url = row.Field<string>("Url");
            ExternalId = row.Field<string>("ExternalId");
            CreatedAt = row.Field<DateTime>("CreatedAt");
            Type = row.Field<string>("Type");
            Subject = row.Field<string>("Subject");
            Description = row.Field<string>("Description");
            Priority = row.Field<string>("Priority");
            Status = row.Field<string>("Status");
            SubmitterId = row.Field<int>("SubmitterId");
            AssigneeId = row.Field<int>("AssigneeId");
            OrganizationId = row.Field<int>("OrganizationId");
            Tags = row.Field<List<string>>("Tags");
            HasIncidents = row.Field<bool>("HasIncidents");
            DueAt = row.Field<DateTime?>("DueAt");
            Via = row.Field<string>("Via");
            return this;
        }
    }


}