# MongoDb


# tldr;

When you want to use MongoDb using LINQ syntax, you need to add this namespace `using MongoDB.Driver.Linq;`
Otherwise, you will not be able to get extension methods like `ToListAsync()` that are attached to `IMongoQueryable`.
If you are not getting the `*Async()` methods after adding the namespace, you are probably the wrong extension method.
You need to call methods that return `IMongoQueryable`.


The MongoDb fluent syntax can be neater. (pick your poison).


```cs:Other using statements
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
```



```cs

// res0 is a list of BsonDocument; see below for the strongly typed version
[HttpGet]
public async Task<IEnumerable<string>> GetAsync()
{
    var res0 = await _userCollection.Aggregate()
        .Unwind("roles")
        .Group(new BsonDocument
        {
            { "_id", "$roles" },
            { "count", new BsonDocument("$sum", 1) }
        })
        .ToListAsync();

    return new string[] { "value1", "value2" };
}

// Strongly-typed version

// GET: api/<RoleController>
[HttpGet]
public async Task<List<UserRoleAggregateResult>> GetAsync()
{
    var res0 = await _userCollection.Aggregate()
        .Unwind("roles")
        .Group<UserRoleAggregateResult>(new BsonDocument {
                { "_id", "$roles" },
                { "Count", new BsonDocument("$sum", 1) }
            })
        .ToListAsync();

    return res0;
}

// elsewhere

    public class UserRoleAggregateResult
    {
        [BsonElement(elementName: "_id")]
        // Or [BsonElement("_id")]
        public string Role { get; set; }

        [BsonElement(elementName: "Count")]
        // Or [BsonElement("Count")]
        public int Count { get; set; }
    }


```

Note:
    Group requires "_id"
    So in the receiving result data class, you need to designate a field as "_id"


## What I tried

```cs
// In some class
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

    // Just use LINQ!!? But there's no async?
    // See: https://mongodb.github.io/mongo-csharp-driver/2.1/reference/driver/crud/linq/#unwind
    var res8 = _userCollection.AsQueryable()
        .SelectMany(r => r.Roles)
        .GroupBy(r => r)
        .Select(g => new
        {
            Role = g.Key,
            Count = g.Count()
        })
        .ToList()
        ;






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


...

// As advised on MongoDb docs
// Instead of inheriting from IBsonSerializer<XUser> directly, inherit from SerializerBase<XUser>
// XUserSerializer : MongoDB.Bson.Serialization.IBsonSerializer<XUser>
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

```



## Neater way to write?

```cs
public List<BsonDocument> GetPopulationDensityByState(IMongoCollection<BsonDocument> collection)
{
    var aggregate = collection.Aggregate()
        .Match(new BsonDocument("data.totalPop", new BsonDocument("$gt", 1000000)))
        .Unwind("data")
        .Sort(new BsonDocument("data.year", 1))
        .Group(new BsonDocument
        {
            { "_id", "$name" },
            { "pop1990", new BsonDocument("$first", "$data.totalPop") },
            { "pop2010", new BsonDocument("$last", "$data.totalPop") },
            { "areaM" , new BsonDocument("$last", "$areaM") },
            { "division" , new BsonDocument("$last", "$division") }
        })
    .Group(new BsonDocument
    {
        { "_id", "$division" },
        { "_totalPop1990", new BsonDocument("$sum", "$pop1990") },
        { "_totalPop2010", new BsonDocument("$sum", "$pop2010") },
        { "_totalAreaM", new BsonDocument("$sum", "$areaM") },
    })
    .Match(new BsonDocument("_totalAreaM", new BsonDocument("$gt", 100000)))
    .Project(new BsonDocument
    {
        { "_id", 0 },
        { "division", "$_id" } ,
        { "density1990", new BsonDocument("$divide", new BsonArray() {"$_totalPop1990", "$_totalAreaM"}) },
        { "density2010", new BsonDocument("$divide", new BsonArray() {"$_totalPop2010", "$_totalAreaM"}) },
        { "densityDelta", new BsonDocument(
                "$subtract", new BsonArray () {
                    new BsonDocument(
                    "$divide", new BsonArray() { "$_totalPop2010", "$_totalAreaM" }),
                    new BsonDocument(
                    "$divide", new BsonArray() { "$_totalPop1990", "$_totalAreaM" })
                })
        },
        { "totalAreaM", "$_totalAreaM" },
        { "totalPop1990", "$_totalPop1990" },
        { "totalPop2010", "$_totalPop2010" },
    }
    )
    .Sort(new BsonDocument("densityDelta", -1))
    ;
    return aggregate.ToList();
}
```



## Another way to write using LINQ

```cs
// Just use LINQ!!? But there's no async when using aggregates
// See: https://mongodb.github.io/mongo-csharp-driver/2.1/reference/driver/crud/linq/#unwind
var res8 = _userCollection.AsQueryable()
    .SelectMany(r => r.Roles)
    .GroupBy(r => r)
    .Select(g => new
    {
        Role = g.Key,
        Count = g.Count()
    })
    .ToList()
    ;

```


# Reference

https://github.com/mongodb/mongo-csharp-driver/blob/master/tests/MongoDB.Driver.Tests/Samples/AggregationSample.cs

https://mongodb.github.io/mongo-csharp-driver/2.14/reference/driver/crud/linq/

https://chsakell.gitbook.io/mongodb-csharp-docs/aggregation/unwind-stage

https://stackoverflow.com/questions/61719742/getting-mongodb-documents-but-tolistasync-on-iqueryable-throws-exception

https://gist.github.com/rlondner/a6b1f14c110021f15610122abc47a8ae