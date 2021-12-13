using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dn6Poc.TravelFunc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Dn6Poc_TravelFunc.Services;
using Moq;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker;
using System.Net;
using System.IO;
using System.Text.Json;
using Dn6Poc.CountryApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Dn6Poc.TravelFunc.Tests
{
    [TestClass()]
    public class AppUserApiTests
    {
        AppUserApi api = new AppUserApi(new LoggerFactory(), new Mock<IAppUserService>().Object);

        [TestMethod()]
        public void AppUserApiTest()
        {
            //AppUserApi api = new AppUserApi(new LoggerFactory(), new Mock<IAppUserService>().Object);
            //--OR--
            //Mock<IAppUserService> mockService = new Mock<IAppUserService>();
            //AppUserApi api = new AppUserApi(new LoggerFactory(), mockService.Object);

            Assert.IsNotNull(api);
        }

        [TestMethod()]
        public async Task GetAppUserTestAsync()
        {
            Mock<IAppUserService> mockService = new Mock<IAppUserService>();
            mockService.Setup(m => m.FindUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new AppUser());

            AppUserApi api = new AppUserApi(new LoggerFactory(), mockService.Object);

            HttpResponseData result = await api.GetAppUser(MakeMockHttpRequestData<string>(string.Empty), "Some");

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);

        }

        public HttpRequestData MakeMockHttpRequestData<T>(T bodyObject)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddScoped<ILoggerFactory, LoggerFactory>();
            serviceCollection.Configure<WorkerOptions>(_ =>
            {
                _.Serializer = new Azure.Core.Serialization.JsonObjectSerializer();
            });
            //serviceCollection.AddScoped<WorkerOptions>();
            //serviceCollection.AddScoped<IOptions<WorkerOptions>, WorkerOptions>();
            //var serviceProvider = serviceCollection.BuildServiceProvider();

            //var context = new Mock<FunctionContext>();
            //context.SetupProperty(c => c.InstanceServices, serviceProvider);

            Mock<FunctionContext> mockFunctionContext = new Mock<FunctionContext>();
            mockFunctionContext.SetupProperty(m => m.InstanceServices, serviceCollection.BuildServiceProvider());

            var prov = serviceCollection.BuildServiceProvider();

            var result = prov.GetService<IOptions<WorkerOptions>>();


            Mock<HttpResponseData> mockResponse = new Mock<HttpResponseData>(mockFunctionContext.Object);
            

            mockResponse.SetupProperty(m => m.StatusCode);
            mockResponse.SetupProperty(m => m.Headers, new HttpHeadersCollection());
            mockResponse.SetupProperty(m => m.Body, new MemoryStream());

            Mock<HttpRequestData> mockRequest = new Mock<HttpRequestData>(mockFunctionContext.Object);
            
            mockRequest.Setup(m => m.CreateResponse()).Returns(mockResponse.Object);

            mockRequest.Setup(m => m.Body).Returns(
                new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize<T>(bodyObject)))
            );

            return mockRequest.Object;

        }

        [TestMethod()]
        public async Task CreateAppUserTestAsync()
        {
            //Mock<FunctionContext> mockFunctionContext = new Mock<FunctionContext>();

            //Mock<HttpResponseData> mockResponse = new Mock<HttpResponseData>(mockFunctionContext.Object);
            //mockResponse.SetupProperty(m => m.StatusCode);

            //Mock<HttpRequestData> mockRequest = new Mock<HttpRequestData>(mockFunctionContext.Object);
            //mockRequest.Setup(m => m.CreateResponse()).Returns(mockResponse.Object);

            //mockRequest.Setup(m => m.Body).Returns(
            //    new MemoryStream(Encoding.UTF8.GetBytes(JsonSerializer.Serialize<AppUser>(
            //        new AppUser
            //        {
            //            Username = "userA",
            //            Password = "userAPassword"
            //        }
            //    )))
            //);

            Mock<IAppUserService> mockService = new Mock<IAppUserService>();

            AppUserApi api = new AppUserApi(new LoggerFactory(), mockService.Object);

            HttpResponseData result = await api.CreateAppUser(
                MakeMockHttpRequestData<AppUser>(new AppUser
                {
                    Username = "userA",
                    Password = "userAPassword"
                }));

            Assert.AreEqual(HttpStatusCode.Created, result.StatusCode);

        }


        [TestMethod()]
        public async Task CreateAppUserTestAsyncInvalidExample1()
        {
            Mock<FunctionContext> mockFunctionContext = new Mock<FunctionContext>();

            Mock<HttpResponseData> mockResponse = new Mock<HttpResponseData>(mockFunctionContext.Object);

            Mock<HttpRequestData> mockRequest = new Mock<HttpRequestData>(mockFunctionContext.Object);
            mockRequest.Setup(m => m.CreateResponse()).Returns(mockResponse.Object);
            

            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms))
            {
                sw.Write("Just some text");
                ms.Position = 0;
                mockRequest.Setup(m => m.Body).Returns(ms);
            }

            Mock<IAppUserService> mockService = new Mock<IAppUserService>();

            AppUserApi api = new AppUserApi(new LoggerFactory(), mockService.Object);

            await api.CreateAppUser(mockRequest.Object);

            mockResponse.VerifySet(m => m.StatusCode = HttpStatusCode.BadRequest);

        }


        [TestMethod()]
        public async Task CreateAppUserTestAsyncInvalidExample2()
        {
            Mock<FunctionContext> mockFunctionContext = new Mock<FunctionContext>();

            Mock<HttpResponseData> mockResponse = new Mock<HttpResponseData>(mockFunctionContext.Object);
            mockResponse.SetupProperty(m => m.StatusCode);

            Mock<HttpRequestData> mockRequest = new Mock<HttpRequestData>(mockFunctionContext.Object);
            mockRequest.Setup(m => m.CreateResponse()).Returns(mockResponse.Object);


            using (MemoryStream ms = new MemoryStream())
            using (StreamWriter sw = new StreamWriter(ms))
            {
                sw.Write("Just some text");
                ms.Position = 0;
                mockRequest.Setup(m => m.Body).Returns(ms);
            }

            Mock<IAppUserService> mockService = new Mock<IAppUserService>();

            AppUserApi api = new AppUserApi(new LoggerFactory(), mockService.Object);

            var result = await api.CreateAppUser(mockRequest.Object);

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [TestMethod()]
        public async Task CreateAppUserTestAsyncInvalidExample3()
        {
            HttpResponseData result = await api.CreateAppUser(
                MakeMockHttpRequestData<string>("asd"));

            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        //[TestMethod()]
        //public void UpdateAppUserTest()
        //{
            
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void DeleteAppUserTest()
        //{
        //    Assert.Fail();
        //}
    }
}