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

        _logger.LogInformation("CountryService created");
    }

    public void Get(HttpContext context)
    {
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
