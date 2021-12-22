using Dn6Poc.DocuMgmtPortal.Api.Requests;
using Dn6Poc.DocuMgmtPortal.Models;
using Dn6Poc.DocuMgmtPortal.MongoEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dn6Poc.DocuMgmtPortal.Api
{
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

        private readonly IMongoCollection<User> _userCollection;

        public RoleController(ILogger<UserController> logger, IMongoCollection<User> userCollection)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _userCollection = userCollection;
        }


        // GET: api/<RoleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            // https://stackoverflow.com/questions/21509045/mongodb-group-by-array-inner-elements

            //_userCollection.Aggregate([
            //    { $project: { _id: 0, roles: 1 } },
            //]);

            // Requires input and output class
            //PipelineDefinition pipeline = new BsonDocument[]
            //{
            //    new BsonDocument { { "$match", new BsonDocument("x", 1) } },
            //    new BsonDocument { { "$sort", new BsonDocument("y", 1) } }
            //};


            //ProjectionDefinition<User> projection = "{ id: 0, roles: 1 }";

            //ProjectionDefinition<User> groupProjection = "{ _id: \"$roles\", count: { $sum : 1 } }";


            //_userCollection.AggregateToCollection()

            //var res = _userCollection.Aggregate()
            //    //.Project<User>(projection)
            //    .Unwind<User>("roles")
            //    //.Group<User>(groupProjection)
            //    .ToList();
            //    ;

            //var pipeline = collection.Aggregate().AppendStage<BsonDocument>(redact).AppendStage<BsonDocument>(project);
            //var list = pipeline.ToList();

            //_userCollection.Aggregate()
            //    .AppendStage<BsonDocument>(new BsonDocument()
            //    {
            //    })

            //{$project: { _id: 0, roles: 1 } },
            //{$unwind: "$roles" },
            //{$group: { _id: "$roles", count: {$sum: 1} }

            //var res = _userCollection.Aggregate()
            //    .Project(r => new { Id = r.Id, Roles = r.Roles})
            //    //.ToList()
            //    ;




            // PipelineDefinitionBuilder



            ProjectionDefinition<User> projection = "{ id: 1, roles: 1 }";
            ProjectionDefinition<BsonDocument, UserRoleAggregateResult> groupProjection = new BsonDocument {
                     { "_id", "$roles" },
                     { "Population", new BsonDocument("$sum", 1) }
                 };


            var xx = new BsonDocument {
                     { "_id", "$roles" },
                     { "count", new BsonDocument("$sum", 1) }
                 };

            // Works
            var res = _userCollection.Aggregate()
                .Unwind("roles")
                .Group(new BsonDocument { { "_id", "$roles" }, { "count", new BsonDocument("$sum", 1) } })
                .ToList();

            // Works
            var res3 = _userCollection.Aggregate()
                .Unwind(r => r.Roles)
                .Group(new BsonDocument { { "_id", "$roles" }, { "count", new BsonDocument("$sum", 1) } })
                .ToList();

            var res4 = _userCollection.Aggregate()
                .Unwind(r => r.Roles)
                .Group(groupProjection)
                .ToList();

            var res5 = _userCollection.Aggregate()
                .Unwind(r => r.Roles)
                .Group<UserRoleAggregateResult>(groupProjection)
                .ToList();

            //_userCollection.Find(a => a.)
            

            var res6 = _userCollection.Aggregate()
                .Unwind(r => r.Roles)
                .Group(
                    new BsonDocument("_id", "$roles")
                    .Add("population", new BsonDocument("$sum", 1))
                )
                .ToList();

            // Compiles but wrong
            //var res7 = _userCollection.Aggregate()
            //    .Group(a => a.Roles, g => new
            //    {
            //        Rsult = g.Count()
            //    }).ToList();

            var res7 = _userCollection.Aggregate()
                .Unwind<XUser>("roles")
                .ToList();

            AggregateUnwindOptions<XUser> a = new AggregateUnwindOptions<XUser>();
            a.ResultSerializer = new XUserSerializer();

            //var res8 = _userCollection.Aggregate()
            //    .Unwind<XUser>("roles", a)
            //    .Group(x => x._id, g => new
            //    {
            //        Id = g.Key,
            //        Count = g.Key
            //    })
            //    //.Group(b => b, g => new
            //    //{
            //    //    Rsult = "placeholder"
            //    //})
            //    .ToList();

            // Just use LINQ!!?
            // See: https://mongodb.github.io/mongo-csharp-driver/2.1/reference/driver/crud/linq/#unwind
            var res8 = _userCollection.AsQueryable()
                .SelectMany(r => r.Roles)
                .GroupBy(r => r)
                .Select(g => new
                {
                    Role = g.Key,
                    Count = g.Count()
                })
                .ToList();



            //            var x = _userCollection.Aggregate(new BsonArray
            //{
            //    new BsonDocument("$project",
            //    new BsonDocument
            //        {
            //            { "_id", 0 },
            //            { "roles", 1 }
            //        }),
            //    new BsonDocument("$unwind",
            //    new BsonDocument("path", "$roles")),
            //    new BsonDocument("$group",
            //    new BsonDocument
            //        {
            //            { "_id", "$roles" },
            //            { "count",
            //    new BsonDocument("$sum", 1) }
            //        })
            //});



            //db.articles.aggregate({ "$project" : { "author" : 1, "_id" : 0} })  

            return new string[] { "value1", "value2" };
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<RoleController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UserRoleActionRequest request)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, request.Id);

            UpdateDefinition<User>? update;

            if (request.Action == UserRoleAction.Add)
                update = Builders<User>.Update.AddToSet(x => x.Roles, request.Role);
            else // (request.Action == UserRoleAction.Remove)
                update = Builders<User>.Update.PullAll(x => x.Roles, new List<string> { request.Role });

            var updateResult = await _userCollection.UpdateOneAsync(filter, update);

            return Ok(updateResult);

        }

        // DELETE api/<RoleController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }


    //public class XUserSerializer : MongoDB.Bson.Serialization.IBsonSerializer<XUser>
    //{
    //    public Type ValueType => throw new NotImplementedException();

    //    public XUser Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, XUser value)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void Serialize(BsonSerializationContext context, BsonSerializationArgs args, object value)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    object IBsonSerializer.Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}

    public class XUserSerializer : MongoDB.Bson.Serialization.Serializers.SerializerBase<XUser>
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, XUser value)
        {
            base.Serialize(context, args, value);
        }

        public override XUser Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
        {
            XUser result = new XUser();
            
            context.Reader.ReadStartDocument();
            result._id = context.Reader.ReadObjectId();
            result.Username = context.Reader.ReadString();
            result.Password = context.Reader.ReadString(); 
            result.Email = context.Reader.ReadString();
            result.FirstName = context.Reader.ReadString();
            result.LastName = context.Reader.ReadString();
            result.Status = (UserStatus)context.Reader.ReadInt32();
            result.Roles = context.Reader.ReadString();

            context.Reader.ReadEndDocument();

            return result;


            //context.Reader.State
            //context.Reader.CurrentBsonType

            //return base.Deserialize(context, args);
        }
    }


}
