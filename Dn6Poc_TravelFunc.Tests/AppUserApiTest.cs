using Dn6Poc.CountryApi.Models;
using Dn6Poc.TravelFunc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

    //[TestMethod]
    //public async Task TestMethod2()
    //{
    //    var serviceCollection = new ServiceCollection();
    //    serviceCollection.AddScoped<ILoggerFactory, LoggerFactory>();
    //    var serviceProvider = serviceCollection.BuildServiceProvider();

    //    var context = new Mock<FunctionContext>();
    //    context.SetupProperty(c => c.InstanceServices, serviceProvider);

    //    var byteArray = Encoding.ASCII.GetBytes("test");
    //    var bodyStream = new MemoryStream(byteArray);

    //    var request = new Mock<HttpRequestData>(context.Object);


    //    AppUserApi api = new AppUserApi(mockLoggerFactory.Object, mockMongoClient.Object);
    //    //var result = await MyFunction.Run(request.Object, context.Object);

    //    var result = await api.CreateAppUser(request.Object);

    //}

    [TestMethod]
    public async Task TestMethod1()
    {
        System.Environment.SetEnvironmentVariable("mongoDb:safeTravel", "SOME");

        var mockLoggerFactory = new Mock<ILoggerFactory>();
        var mockMongoClient = new Mock<IMongoClient>();
        var mockDatabase = new Mock<IMongoDatabase>();
        var mockAppUserCollection = new Mock<IMongoCollection<AppUser>>();

        LoggerFactory fact = new LoggerFactory();
        fact.CreateLogger<AppUser>();

        //var mockLogger = new Mock<ILogger<AppUserApi>>();
        //mockLogger.Setup(m => m.LogInformation("AppUser created.")).Verifiable();

        //mockLoggerFactory.Setup(m => m.CreateLogger<AppUserApi>()).Returns(mockLogger.Object);
        mockMongoClient.Setup(m => m.GetDatabase("safe_travel", null)).Returns(mockDatabase.Object);
        mockDatabase.Setup(m => m.GetCollection<AppUser>("appUser", null)).Returns(mockAppUserCollection.Object);

        AppUserApi api = new AppUserApi(new LoggerFactory(), mockMongoClient.Object);

        AppUser inputAppUser = new AppUser
        {
            Username = "userA",
            Password = "userAPassword"
        };


        mockAppUserCollection.Setup(
            m => m.InsertOne(inputAppUser, null, default)
        ).Verifiable();



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

        //DefaultHttpContext ctx = new DefaultHttpContext();
        
        var mockRequest = new Mock<HttpRequestData>(mockFunctionContext.Object);

        //var mockRequest = new Mock<HttpRequestData>()

        var mockResponse = new Mock<HttpResponseData>(mockFunctionContext.Object);
        //mockResponse.Setup(m => m.StatusCode).Returns(HttpStatusCode.Created);

        mockRequest.Setup(m => m.Body).Returns(inputStream);
        mockRequest.Setup(m => m.CreateResponse()).Returns(mockResponse.Object);

        //mockRequest.Setup(m => HttpRequestDataExtensions.CreateResponse(m, HttpStatusCode.Created)).Returns(mockResponse.Object);

        var response = await api.CreateAppUser(mockRequest.Object);


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


    [TestMethod]
    public async Task TestMethod2()
    {
        AppUser inputAppUser = new AppUser
        {
            Username = "userA",
            Password = "userAPassword"
        };

        var mockMongoClient = new Mock<IMongoClient>();
        var mockDatabase = new Mock<IMongoDatabase>();
        var mockAppUserCollection = new Mock<IMongoCollection<AppUser>>();
        mockAppUserCollection.Setup(m => m.InsertOne(inputAppUser, null, default)).Verifiable();
        mockDatabase.Setup(m => m.GetCollection<AppUser>("appUser", null)).Returns(mockAppUserCollection.Object);
        mockMongoClient.Setup(m => m.GetDatabase("safe_travel", null)).Returns(mockDatabase.Object);
        
        AppUserApi api = new AppUserApi(new LoggerFactory(), mockMongoClient.Object);

        var mockFunctionContext = new Mock<FunctionContext>();

        var mockResponse = new Mock<HttpResponseData>(mockFunctionContext.Object);
        mockResponse.Setup(m => m.StatusCode).Returns(HttpStatusCode.OK);

        var mockRequest = new Mock<HttpRequestData>(mockFunctionContext.Object);
        mockRequest.Setup(m => m.CreateResponse()).Returns(mockResponse.Object);
        using (MemoryStream ms = new MemoryStream())
        using (StreamWriter sw = new StreamWriter(ms))
        {
            sw.Write(JsonSerializer.Serialize<AppUser>(inputAppUser));
            ms.Position = 0;
            mockRequest.Setup(m => m.Body).Returns(ms);
        }

        var response = await api.CreateAppUser(mockRequest.Object);

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

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


//public class aa : HttpRequestData
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