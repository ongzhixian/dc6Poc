using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dn6Poc.CountryApi.Models;

public partial class AppUser
{
    [JsonPropertyName("id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [JsonPropertyName("username")]
    [BsonElement("username")]
    public string Username { get; set; }

    [JsonPropertyName("password")]
    [BsonElement("password")]
    public string Password { get; set; }
}