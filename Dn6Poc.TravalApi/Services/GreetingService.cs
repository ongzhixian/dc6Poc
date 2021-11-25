public class GreetingService
{
    private readonly IConfiguration _configuration;

    public GreetingService(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public string SayHello(string name)
    {
        return $"Hello {name} from {_configuration["Application:Author"]}";
    }
}