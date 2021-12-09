using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Dn6Poc.CountryApi.Models;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Dn6Poc.TravelFunc
{
    public class AppUserApi
    {
        private readonly ILogger _logger;
        private readonly IMongoCollection<AppUser> _appUsers;


        public AppUserApi(ILoggerFactory loggerFactory, IMongoClient client)
        {
            _logger = loggerFactory.CreateLogger<AppUserApi>();

            //var client = new MongoClient(System.Environment.GetEnvironmentVariable("mongoDb:safeTravel"));
            var database = client.GetDatabase("safe_travel");
            _appUsers = database.GetCollection<AppUser>("appUser");
        }

        [Function("ListAppUser")]
        public HttpResponseData ListAppUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "AppUser")] HttpRequestData req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }

        [Function("GetAppUser")]
        public async Task<HttpResponseData> GetAppUser([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "AppUser/{id}")] HttpRequestData req,
        string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                _logger.LogError("Invalid id.", id);
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                IAsyncCursor<AppUser> cursor = await _appUsers.FindAsync(m => m.Id == id);

                AppUser result = await cursor.FirstOrDefaultAsync();

                if (result == null)
                {
                    _logger.LogInformation("AppUser not found. {id}", id);
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }

                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync<AppUser>(result);

                _logger.LogInformation("AppUser found. {id}", id);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot get AppUser. {id}", id);
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        [Function("CreateAppUser")]
        public async Task<HttpResponseData> CreateAppUser([HttpTrigger(AuthorizationLevel.Function, "post", Route = "AppUser")] HttpRequestData req)
        {
            AppUser user;

            try
            {
                user = await JsonSerializer.DeserializeAsync<AppUser>(req.Body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid message body. {0}", req);
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                _appUsers.InsertOne(user);

                _logger.LogInformation("AppUser created.");
                return req.CreateResponse(HttpStatusCode.Created);
            }
            catch (MongoWriteException ex)
            {
                if (ex.WriteError.Category == ServerErrorCategory.DuplicateKey)
                {
                    _logger.LogInformation(ex, "DuplicateKey {0}", user);
                    return req.CreateResponse(HttpStatusCode.OK);
                }

                _logger.LogWarning(ex, "Unhandled MongoWriteException.");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot insert to database. {0}", user);
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        [Function("UpdateAppUser")]
        public async Task<HttpResponseData> UpdateAppUser([HttpTrigger(AuthorizationLevel.Function, "put", Route = "AppUser")] HttpRequestData req)
        {
            AppUser user;

            try
            {
                user = await JsonSerializer.DeserializeAsync<AppUser>(req.Body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid message body. {0}", req);
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                var filter = Builders<AppUser>.Filter.Eq(x => x.Username, user.Username);

                AppUser result = _appUsers.FindOneAndUpdate<AppUser>(
                    m => m.Username == user.Username,
                    Builders<AppUser>.Update.Set(m => m.Password, user.Password)
                    );

                if (result == null)
                {
                    _logger.LogInformation("AppUser not found. {req}", req);
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }

                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync<AppUser>(result);

                _logger.LogInformation("AppUser updated. {result}", result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot update AppUser. {req}", req);
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }


        [Function("DeleteAppUser")]
        public async Task<HttpResponseData> DeleteAppUser([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "AppUser")] HttpRequestData req)
        {
            AppUser user;

            try
            {
                user = await JsonSerializer.DeserializeAsync<AppUser>(req.Body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Invalid message body. {0}", req);
                return req.CreateResponse(HttpStatusCode.BadRequest);
            }

            try
            {
                AppUser result = await _appUsers.FindOneAndDeleteAsync<AppUser>(m => m.Username == user.Username);

                if (result == null)
                {
                    _logger.LogInformation("AppUser not found. {req}", req);
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }

                HttpResponseData response = req.CreateResponse(HttpStatusCode.OK);
                await response.WriteAsJsonAsync<AppUser>(result);

                _logger.LogInformation("AppUser deleted. {result}", result);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot delete AppUser. {req}", req);
                return req.CreateResponse(HttpStatusCode.InternalServerError);
                throw;
            }

        }

    }
}
