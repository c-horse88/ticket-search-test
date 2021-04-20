using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using System.Data;
using System;
namespace TicketSearch.Model
{

    public class JoinedUser : User
    {
        [JsonPropertyName("_id")]
        public new int Id { get; set; }

        [JsonPropertyName("url")]
        public new string Url { get; set; }

        [JsonPropertyName("external_id")]
        public new string ExternalId { get; set; }

        [JsonPropertyName("name")]
        public new string Name { get; set; }

        [JsonPropertyName("alias")]
        public new string Alias { get; set; }

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        public new DateTime? CreatedAt { get; set; }

        [JsonPropertyName("active")]
        public new bool Active { get; set; }

        [JsonPropertyName("verified")]
        public new bool Verified { get; set; }

        [JsonPropertyName("shared")]
        public new bool Shared { get; set; }

        [JsonPropertyName("locale")]
        public new string Locale { get; set; }

        [JsonPropertyName("timezone")]
        public new string Timezone { get; set; }

        [JsonPropertyName("last_login_at")]
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        public new DateTime? LastLoginAt { get; set; }

        [JsonPropertyName("email")]
        public new string Email { get; set; }

        [JsonPropertyName("phone")]
        public new string Phone { get; set; }

        [JsonPropertyName("signature")]
        public new string Signature { get; set; }


        [JsonPropertyName("tags")]
        public new List<string> Tags { get; set; }

        [JsonPropertyName("suspended")]
        public new bool Suspended { get; set; }

        [JsonPropertyName("role")]
        public new string Role { get; set; }



        [JsonPropertyName("organization_id")]
        [JsonIgnore]
        public new int OrganizationId { get; set; }

        public Organization Organization { get; set; }
        [JsonPropertyName("assigned_tickets")]
        public List<Ticket> AssignedTickets { get; set; }
        [JsonPropertyName("submitted_tickets")]
        public List<Ticket> SubmittedTickets { get; set; }



        public new JoinedUser Map(DataRow row)
        {
            Id = row.Field<int>("Id");
            Url = row.Field<string>("Url");
            ExternalId = row.Field<string>("ExternalId");
            Name = row.Field<string>("Name");
            Alias = row.Field<string>("Alias");
            CreatedAt = row.Field<DateTime>("CreatedAt");
            Active = row.Field<bool>("Active");
            Verified = row.Field<bool>("Verified");
            Shared = row.Field<bool>("Shared");
            Locale = row.Field<string>("Locale");
            Timezone = row.Field<string>("Timezone");
            LastLoginAt = row.Field<DateTime>("LastLoginAt");
            Email = row.Field<string>("Email");
            Phone = row.Field<string>("Phone");
            Signature = row.Field<string>("Signature");
            OrganizationId = row.Field<int>("OrganizationId");
            Tags = row.Field<List<string>>("Tags");
            Suspended = row.Field<bool>("Suspended");
            Role = row.Field<string>("Role");
            return this;
        }
    }


}