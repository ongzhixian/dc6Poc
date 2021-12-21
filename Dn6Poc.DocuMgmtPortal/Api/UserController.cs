using Dn6Poc.DocuMgmtPortal.Api.Requests;
using Dn6Poc.DocuMgmtPortal.MongoEntities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dn6Poc.DocuMgmtPortal.Api
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(JwtBearerDefaults.AuthenticationScheme)]
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;

        private readonly IMongoCollection<User> _userCollection;

        public UserController(ILogger<UserController> logger, IMongoCollection<User> userCollection)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _userCollection = userCollection;
        }

        
        // GET: api/<UserController>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            //var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            
            //string startswith = query["startswith"];

            //var filter = Builders<User>.Filter.Regex(x => x.Name, new BsonRegularExpression($"^{startswith}", "i"));
            var filter = Builders<User>.Filter.Empty;

            ProjectionDefinition<User> projection = "{ id: 0 }";

            var cursor = await _userCollection.FindAsync<User>(filter, new FindOptions<User, User>() { 
                //Projection = projection,
                Sort = Builders<User>.Sort.Ascending(x => x.FirstName),
                Limit = 10,
                Skip = 0
            });

            var result = await cursor.ToListAsync();

            return Ok(result);

            //await response.WriteAsJsonAsync(result);

            //return response;

            //_userCollection.
            //return new string[] { "value1", "value2" };
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {

            //var filter = Builders<User>.Filter.Regex(x => x.Name, new BsonRegularExpression($"^{id}$", "i"));

            //ProjectionDefinition<Country> projection = "{ id: 0 }";

            //var response = req.CreateResponse(HttpStatusCode.OK);

            //var cursor = await _countries.FindAsync<Country>(filter, new FindOptions<Country, Country>() { Projection = projection });

            //var result = await cursor.ToListAsync();


            return "value";
        }

        
        
        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserRequest value)
        {
            User newUser = new User
            {
                Username = value.Username,
                Password = value.Password,
                FirstName = value.FirstName,
                LastName = value.LastName,
                Email = value.Email,
                Status = Models.UserStatus.Active
            };
            
            await _userCollection.InsertOneAsync(newUser);

            return CreatedAtAction(nameof(Post), newUser);
        }

        // PUT api/<UserController>/5
        //[HttpPut("{id}")]
        [HttpPut]
        public async Task<IActionResult> PutAsync([FromBody] UpdateUserStatusRequest request)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, request.Id);
            
            var update = Builders<User>.Update.Set(x => x.Status, request.UserStatus);

            var updateResult = await _userCollection.UpdateOneAsync(filter, update);

            return Ok(updateResult);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
