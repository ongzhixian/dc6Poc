using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
//using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Dn6Poc_TravelFunc.Services;
using MongoDB.Driver;

namespace Dn6Poc_TravelFunc
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    //services.AddScoped<ITravelFuncService, AppUserService>(a => new AppUserService(
                    //    new MongoClient(System.Environment.GetEnvironmentVariable("mongoDb:safeTravel"))));
                    
                    services.AddScoped<IMongoClient>(_ => new MongoClient(System.Environment.GetEnvironmentVariable("mongoDb:safeTravel")));
                    services.AddScoped<AppUserService, AppUserService>();
                })
                .Build();

            host.Run();
        }
    }
}