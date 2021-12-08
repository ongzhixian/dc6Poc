using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dn6Poc.CountryApi.Models;

public partial class Country
{
    [JsonPropertyName("id")]
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    [BsonElement("name")]
    public string Name { get; set; }

    [JsonPropertyName("code")]
    [BsonElement("code")]
    public string Code { get; set; }
}