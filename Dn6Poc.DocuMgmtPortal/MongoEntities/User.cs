using Dn6Poc.DocuMgmtPortal.Models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace Dn6Poc.DocuMgmtPortal.MongoEntities
{
    public class User
    {
        [JsonPropertyName("id")]
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("username")]
        [BsonElement("username")]
        public string Username { get; init; } = string.Empty;

        [JsonPropertyName("password")]
        [BsonElement("password")]
        public string Password { get; init; } = string.Empty;


        [JsonPropertyName("email")]
        [BsonElement("email")]
        public string Email { get; init; } = string.Empty;

        [JsonPropertyName("firstName")]
        [BsonElement("firstName")]
        public string FirstName { get; init; } = string.Empty;


        [JsonPropertyName("lastName")]
        [BsonElement("lastName")]
        public string LastName { get; init; } = string.Empty;

        [JsonPropertyName("status")]
        [BsonElement("status")]
        public UserStatus Status { get; init; }

        [JsonPropertyName("roles")]
        [BsonElement("roles")]
        public string[] Roles { get; set; } = new string[] { };

        //
        // Record created date
        // Record last update
        // Password last update
        // Audit

    }
}
