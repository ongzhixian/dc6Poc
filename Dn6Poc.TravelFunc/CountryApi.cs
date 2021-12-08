using System.Collections.Generic;
using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Dn6Poc.CountryApi.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace Dn6Poc.TravelFunc
{
    public class CountryApi
    {
        private readonly ILogger _logger;

        private readonly IMongoCollection<Country> _countries;

        public CountryApi(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CountryApi>();

                
            var client = new MongoClient(System.Environment.GetEnvironmentVariable("mongoDb:safeTravel"));
            var database = client.GetDatabase("safe_travel");
            _countries = database.GetCollection<Country>("country");

            _logger.LogInformation("CountryService created");

        }

        [Function("CountryExample")]
        public async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "Example")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            
            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            string startswith = query["startswith"];

            _logger.LogInformation($"GetCountriesAsync called; startswith: [{startswith}]");

            var filter = Builders<Country>.Filter.Regex(x => x.Name, new BsonRegularExpression($"^{startswith}", "i"));

            ProjectionDefinition<Country> projection = "{ id: 0 }";

            var response = req.CreateResponse(HttpStatusCode.OK);

            response.Headers.Add("Content-Type", "application/json; charset=utf-8");

            // Synchronous
            // response.WriteString(
            //     System.Text.Json.JsonSerializer.Serialize(
            //         _countries.Find(filter).Project<Country>(projection).ToList()));


            // Asynchronous
            var cursor = await _countries.FindAsync<Country>(filter, new FindOptions<Country, Country>() { Projection = projection });

            var result = await cursor.ToListAsync();

            response.WriteString(
                System.Text.Json.JsonSerializer.Serialize(
                    result
            ));

            return response;

        }

        [Function("ListCountry")]
        public async Task<HttpResponseData> List(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route ="country")] 
            HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var query = System.Web.HttpUtility.ParseQueryString(req.Url.Query);
            string startswith = query["startswith"];

            _logger.LogInformation($"GetCountriesAsync called; startswith: [{startswith}]");

            var filter = Builders<Country>.Filter.Regex(x => x.Name, new BsonRegularExpression($"^{startswith}", "i"));

            ProjectionDefinition<Country> projection = "{ id: 0 }";

            var response = req.CreateResponse(HttpStatusCode.OK);
            
            var cursor = await _countries.FindAsync<Country>(filter, new FindOptions<Country, Country>() { Projection = projection });

            var result = await cursor.ToListAsync();

            await response.WriteAsJsonAsync(result);

            return response;

        }


        [Function("GetCountry")]
        public async Task<HttpResponseData> GetCountry(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route ="country/{id}")] 
            HttpRequestData req, 
            string id)
        {
            _logger.LogInformation("GetCountry - C# HTTP trigger function processed a request.");

            _logger.LogInformation($"GetCountry- GetCountriesAsync called; equals: [{id}]");

            var filter = Builders<Country>.Filter.Regex(x => x.Name, new BsonRegularExpression($"^{id}$", "i"));
            
            ProjectionDefinition<Country> projection = "{ id: 0 }";

            var response = req.CreateResponse(HttpStatusCode.OK);
            
            var cursor = await _countries.FindAsync<Country>(filter, new FindOptions<Country, Country>() { Projection = projection });

            var result = await cursor.ToListAsync();

            await response.WriteAsJsonAsync(result);

            return response;

        }
    }
}
