using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Dn6Poc.CountryApi.Models;

public partial class Country
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string? Name { get; set; }

    [BsonElement("code")]
    public string? Code { get; set; }
}