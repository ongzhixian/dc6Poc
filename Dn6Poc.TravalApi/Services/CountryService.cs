public class CountryService
{
    private ILogger<CountryService> _logger;
    private readonly IConfiguration _configuration;

    public CountryService(
        IConfiguration configuration
        , ILogger<CountryService> logger
    )
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        // var mongoClient = new MongoClient(
        //     bookStoreDatabaseSettings.Value.ConnectionString);

        // var mongoDatabase = mongoClient.GetDatabase(
        //     bookStoreDatabaseSettings.Value.DatabaseName);

        // _booksCollection = mongoDatabase.GetCollection<Book>(
        //     bookStoreDatabaseSettings.Value.BooksCollectionName);

        _logger.LogInformation("My connection string is: {0}", _configuration["mongoDb:safeTravel"]);
        // configuration["mongoDb:safeTravel"]


        _logger.LogInformation("CountryService created");
    }

    public async Task Get(HttpContext context)
    {
        // Dn6Poc.TravalApi.Models.MongoDb.Country a = new Dn6Poc.TravalApi.Models.MongoDb.Country
        // {
        //     Id = "1",
        //     Name = "India",
        //     Code = "IN"
        // };
        
        // context.Response.ContentType = "plain/text";
        // await context.Response.WriteAsJsonAsync<Dn6Poc.TravalApi.Models.MongoDb.Country>(a);
        await context.Response.WriteAsync("async asdasd");

    }

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
