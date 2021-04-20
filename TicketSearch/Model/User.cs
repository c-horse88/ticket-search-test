using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;
using System.Data;
using System;
namespace TicketSearch.Model
{

    public class User
    {
        [JsonPropertyName("_id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("external_id")]
        public string ExternalId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("alias")]
        public string Alias { get; set; }

        [JsonPropertyName("created_at")]
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        public DateTime? CreatedAt { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("verified")]
        public bool Verified { get; set; }

        [JsonPropertyName("shared")]
        public bool Shared { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("last_login_at")]
        [JsonConverter(typeof(Converters.DateTime.ConverterNullable))]
        public DateTime? LastLoginAt { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("signature")]
        public string Signature { get; set; }

        [JsonPropertyName("organization_id")]
        public int OrganizationId { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("suspended")]
        public bool Suspended { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        public static List<User> Get(string data)
        {
            return JsonSerializer.Deserialize<List<User>>(data, new JsonSerializerOptions { IgnoreNullValues = true });
        }
        public User Map(DataRow row)
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