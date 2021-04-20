using System;
using System.Data;
using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace TicketSearch.Model
{
    public class JoinedTicket : Ticket
    {

        [JsonPropertyName("_id")]
        public new string Id { get; set; }

        [JsonPropertyName("url")]
        public new string Url { get; set; }

        [JsonPropertyName("external_id")]
        public new string ExternalId { get; set; }

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        public new DateTime? CreatedAt { get; set; }

        [JsonPropertyName("type")]
        public new string Type { get; set; }

        [JsonPropertyName("subject")]
        public new string Subject { get; set; }

        [JsonPropertyName("description")]
        public new string Description { get; set; }

        [JsonPropertyName("priority")]
        public new string Priority { get; set; }

        [JsonPropertyName("status")]
        public new string Status { get; set; }

        [JsonPropertyName("tags")]
        public new List<string> Tags { get; set; }

        [JsonPropertyName("has_incidents")]
        public new bool HasIncidents { get; set; }

        [JsonPropertyName("due_at")]
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        public new DateTime? DueAt { get; set; }

        [JsonPropertyName("via")]
        public new string Via { get; set; }
        [JsonPropertyName("submitter_id")]
        [JsonIgnore]
        public new int SubmitterId { get; set; }

        [JsonPropertyName("assignee_id")]
        [JsonIgnore]
        public new int AssigneeId { get; set; }

        [JsonPropertyName("organization_id")]
        [JsonIgnore]
        public new int OrganizationId { get; set; }

        [JsonPropertyName("submitter")]
        public User Submitter { get; set; }

        [JsonPropertyName("assignee")]
        public User Assignee { get; set; }
        public Organization Organization { get; set; }

        public new JoinedTicket Map(DataRow row)
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