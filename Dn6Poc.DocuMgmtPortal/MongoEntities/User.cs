﻿using Dn6Poc.DocuMgmtPortal.Models;
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
        public string Id { get; set; }

        [JsonPropertyName("username")]
        [BsonElement("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        [BsonElement("password")]
        public string Password { get; set; }


        [JsonPropertyName("email")]
        [BsonElement("email")]
        public string Email { get; set; }

        [JsonPropertyName("firstName")]
        [BsonElement("firstName")]
        public string FirstName { get; set; }


        [JsonPropertyName("lastName")]
        [BsonElement("lastName")]
        public string LastName { get; set; }

        [JsonPropertyName("status")]
        [BsonElement("status")]
        public UserStatus Status { get; set; }

        [JsonPropertyName("roles")]
        [BsonElement("roles")]
        public string[] Roles { get; set; }

        //
        // Record created date
        // Record last update
        // Password last update
        // Audit

    }

    public class XUser 
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId _id { get; set; }

        //[BsonElement(elementName: "_id")]
        //public string XID { get; set; }

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }


        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }


        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("status")]
        public UserStatus Status { get; set; }

        [BsonElement("roles")]
        public string Roles { get; set; }
    }


    public class UserRoleAggregateResult
    {
        [BsonElement(elementName: "_id")]
        public string XRole { get; set; }

        [BsonElement(elementName: "Population")]
        public int XCount { get; set; }
    }
}
