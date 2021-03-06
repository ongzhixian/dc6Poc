using Dn6Poc.TravalApi.Models.MongoDb;
using MongoDB.Bson;
using MongoDB.Driver;

public class CountryService
{
    private ILogger<CountryService> _logger;
    private readonly IConfiguration _configuration;
    private readonly IMongoCollection<Country> _countries;

    public CountryService(
        IConfiguration configuration
        , ILogger<CountryService> logger
    )
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        var client = new MongoClient(_configuration["mongoDb:safeTravel"]);
        var database = client.GetDatabase("safe_travel");
        _countries = database.GetCollection<Country>("country");

        _logger.LogInformation("CountryService created");
    }

    public async Task Get(HttpContext context)
    {
        var result = _countries.Find(f => true).ToList();
        await context.Response.WriteAsJsonAsync<List<Country>>(result);
    }

    public async Task<List<Country>> GetCountriesAsync()
    {
        var cursor = await _countries.FindAsync<Country>(FilterDefinition<Country>.Empty);

        return await cursor.ToListAsync();
    }

    public async Task<Country> GetCountry(string id)
    {
        var filter = Builders<Country>.Filter.Regex(x => x.Name, new BsonRegularExpression($"^{id}", "i"));

        var cursor = await _countries.FindAsync<Country>(filter, new FindOptions<Country, Country>
        {
            Sort = Builders<Country>.Sort.Ascending(x => x.Name)
        });

        return await cursor.FirstOrDefaultAsync();
    }


    // public async Task<List<Country>> GetCountryList()
    // {
    //     // var result = _countries.Find(f => true).ToList();
    //     // FilterDefinition<BsonDocument> filter = FilterDefinition<BsonDocument>.Empty;

    //     var cursor =  await _countries.FindAsync<List<Country>>(FilterDefinition<Country>.Empty);
    //     while cursor.MoveNextAsync().Result;
    // }

    public void Post(HttpContext context)
    {

    }

    public void Put(HttpContext context)
    {

    }

    public void Delete(HttpContext context)
    {

    }
}
