using Dn6Poc.CountryApi.Models;
using Dn6Poc.TravelFunc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Dn6Poc_TravelFunc.Tests;

[TestClass]
public class AppUserApiTest
{
    //public class MyHttpRequestData : HttpRequestData
    //{
    //    public override Stream Body => throw new NotImplementedException();

    //    public override HttpHeadersCollection Headers => throw new NotImplementedException();

    //    public override IReadOnlyCollection<IHttpCookie> Cookies => throw new NotImplementedException();

    //    public override Uri Url => throw new NotImplementedException();

    //    public override IEnumerable<ClaimsIdentity> Identities => throw new NotImplementedException();

    //    public override string Method => throw new NotImplementedException();

    //    public override HttpResponseData CreateResponse()
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    [TestMethod]
    public async Task TestMethod1()
    {
        System.Environment.SetEnvironmentVariable("mongoDb:safeTravel", "SOME");

        var mockLoggerFactory = new Mock<ILoggerFactory>();
        var mockMongoClient = new Mock<IMongoClient>();
        var mockDatabase = new Mock<IMongoDatabase>();
        var mockAppUserCollection = new Mock<IMongoCollection<AppUser>>();

        mockMongoClient.Setup(m => m.GetDatabase("safe_travel", null)).Returns(mockDatabase.Object);
        mockDatabase.Setup(m => m.GetCollection<AppUser>("appUser", null)).Returns(mockAppUserCollection.Object);

        AppUserApi api = new AppUserApi(mockLoggerFactory.Object, mockMongoClient.Object);

        AppUser inputAppUser = new AppUser
        {
            Username = "userA",
            Password = "userAPassword"
        };

        var inputStream = new MemoryStream();
        var writer = new StreamWriter(inputStream);
        writer.Write(JsonSerializer.Serialize<AppUser>(inputAppUser));
        writer.Flush();
        inputStream.Position = 0;


        var mockFunctionContext = new Mock<FunctionContext>();
        //HttpRequestData d = new HttpRequestData(mockFunctionContext.Object);

        //HttpRequestData s = (HttpRequestData)new
        //{
        //    Body = ""
        //};

        DefaultHttpContext ctx = new DefaultHttpContext();

        var mockRequest = new Mock<HttpRequestData>();
        mockRequest.Setup(m => m.Body).Returns(inputStream);

        var response = await api.CreateAppUser(mockRequest);


        //var context = new DefaultHttpContext();
        //var request = context.Request;
        //request.Query = new QueryCollection(CreateDictionary(queryStringKey, queryStringValue));
        //return request;

        //Microsoft.Azure.Functions.Worker.FunctionContext ctx = new Microsoft.Azure.Functions.Worker.FunctionContext(mockLoggerFactory.Object);
        //HttpRequestData d = new HttpRequestData()

        
        //var reqMock = new Mock<HttpRequest>();
        //reqMock.Setup(req => req.Query).Returns(new QueryCollection(query));
        //var stream = new MemoryStream();
        //var writer = new StreamWriter(stream);
        //writer.Write("asd");
        //writer.Flush();
        //stream.Position = 0;
        //reqMock.Setup(req => req.Body).Returns(stream);
        

        //var context = new DefaultHttpContext();

        //api.GetAppUser()

    }

    //public static HttpRequest CreateHttpRequest(string queryStringKey, string queryStringValue)
    //{
    //    var context = new DefaultHttpContext();
    //    var request = context.Request;
    //    request.Query = new QueryCollection(CreateDictionary(queryStringKey, queryStringValue));
    //    return request;
    //}

    //[Fact]
    //public async void Http_trigger_should_return_known_string()
    //{
    //    var request = TestFactory.CreateHttpRequest("name", "Bill");
    //    var response = (OkObjectResult)await MyHttpTrigger.Run(request, logger);
    //    Assert.Equal("Hello, Bill. This HTTP triggered function executed successfully.", response.Value);
    //}
}