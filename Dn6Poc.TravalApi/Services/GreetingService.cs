using Dn6Poc.TravalApi.DbContexts;

public class GreetingService
{
    private ILogger<GreetingService> _logger;
    private readonly IConfiguration _configuration;
    private SafeTravelContext _context;

    public GreetingService(IConfiguration configuration, ILogger<GreetingService> logger, SafeTravelContext context)
    {   
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _context = context ?? throw new ArgumentNullException(nameof(context));

        _logger.LogInformation("GreetingService created");
    }

    public string SayHello(string name)
    {
        var b = this._context.Blogs.FirstOrDefault();

        return $"Hello {name} from {_configuration["Application:Author"]}, {b.Name}";
    }
}