using Dn6Poc.DocuMgmtPortal.Api.Requests;
using Dn6Poc.DocuMgmtPortal.Models;
using Dn6Poc.DocuMgmtPortal.MongoEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.Linq;

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
        public async Task<List<UserRoleAggregateResult>> GetAsync()
        {
            // Using MongoDb fluent syntax
            var res0 = await _userCollection.Aggregate()
                .Unwind("roles")
                .Group<UserRoleAggregateResult>(new BsonDocument {
                     { "_id", "$roles" },
                     { "Count", new BsonDocument("$sum", 1) }
                 })
                .ToListAsync();

            // Pick your poison

            // -- OR -- MongoDb LINQ syntax (variant 1)
            //var res1 = await _userCollection.AsQueryable()
            //    .SelectMany(x => x.Roles)
            //    .GroupBy(r => r)
            //    .Select(r => new UserRoleAggregateResult
            //    {
            //        Role = r.Key,
            //        Count = r.Count()
            //    })
            //    .ToListAsync();

            // -- OR -- MongoDb LINQ syntax (variant 1)
            //var res2 = await _userCollection.AsQueryable()
            //    .SelectMany(x => x.Roles)
            //    .GroupBy(role => role, (role, valueList) => new UserRoleAggregateResult
            //    {
            //        Role = role,
            //        Count = valueList.Count()
            //    })
            //    .ToListAsync();

            return res0;
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
}
