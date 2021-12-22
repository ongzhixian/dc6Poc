using MongoDB.Bson.Serialization.Attributes;

namespace Dn6Poc.DocuMgmtPortal.MongoEntities
{
    public class UserRoleAggregateResult
    {
        [BsonElement("_id")]
        public string Role { get; set; }

        [BsonElement("Count")]
        public int Count { get; set; }
    }
}
