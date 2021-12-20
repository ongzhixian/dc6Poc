using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dn6Poc.DocuMgmtPortal.Api.Requests;
using Dn6Poc.DocuMgmtPortal.MongoEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace Dn6Poc.DocuMgmtPortal.Api;

//public class JwtResponse
//{
//    public string Token { get; set; }
//    public DateTime Expiration { get; set; }
//}


//[ApiController]
//[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private static class Event
    {
        public static readonly EventId LOGIN = new EventId(1, "LOGIN");
    }

    private readonly ILogger<UserController> _logger;

    private readonly IConfiguration _configuration;

    private readonly string _mongoConnectionUrl;

    private readonly IMongoClient _mongoClient;

    private readonly IMongoCollection<User> _userCollection;

    public UserController(ILogger<UserController> logger, IConfiguration configuration, IMongoCollection<User> userCollection)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        _userCollection = userCollection; 

        //_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

        //_mongoConnectionUrl = _configuration.GetValue<string>("mongoDb:safeTravel");

        //IMongoClient _mongoClient = new MongoClient(_mongoConnectionUrl) ;

        //string databaseName = MongoUrl.Create(_mongoConnectionUrl).DatabaseName;

        //IMongoDatabase database = _mongoClient.GetDatabase(databaseName);


        //database.GetCollection<User>("user");

        //    _mongoClient.GetDatabase() ?? throw new Exception("No database");
        //_countries = database.GetCollection<Country>("country");

        _logger.LogInformation("CountryService created");


    }

    [HttpPost]
    public IActionResult Add([FromBody] LoginRequest model)
    {
        // mongoDb:safeTravel




        return Ok();
    }
}
