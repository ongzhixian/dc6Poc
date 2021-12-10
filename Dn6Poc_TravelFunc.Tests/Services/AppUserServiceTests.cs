using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dn6Poc_TravelFunc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;
using Dn6Poc.CountryApi.Models;
using System.Threading;
using System.Linq.Expressions;

namespace Dn6Poc_TravelFunc.Services.Tests
{
    [TestClass()]
    public class AppUserServiceTests
    {
        private IMongoClient GetMockMongoClient(IMongoCollection<AppUser> mongoCollection)
        {
            Mock<IMongoClient> mockMongoClient = new Mock<IMongoClient>();
            Mock<IMongoDatabase> mockDatabase = new Mock<IMongoDatabase>();

            mockDatabase.Setup(m => m.GetCollection<AppUser>("appUser", It.IsAny<MongoCollectionSettings>())).Returns(mongoCollection);
            mockMongoClient.Setup(m => m.GetDatabase("safe_travel", It.IsAny<MongoDatabaseSettings>())).Returns(mockDatabase.Object);

            return mockMongoClient.Object;
        }

        [TestMethod()]
        public void AppUserServiceTest()
        {
            Mock<IMongoCollection<AppUser>> mockAppUserCollection = new Mock<IMongoCollection<AppUser>>();
            AppUserService _service = new AppUserService(new LoggerFactory(), GetMockMongoClient(mockAppUserCollection.Object));

            Assert.IsNotNull(_service);
        }

        [TestMethod()]
        public async Task FindUserByIdAsyncTestAsync()
        {
            // Arrange
            var mockCursor = new Mock<IAsyncCursor<AppUser>>();
            mockCursor.Setup(_ => _.Current).Returns(new List<AppUser> { new AppUser() });
            //mockCursor
            //    .SetupSequence(_ => _.MoveNext(It.IsAny<CancellationToken>()))
            //    .Returns(true)
            //    .Returns(false);
            mockCursor
                .SetupSequence(_ => _.MoveNextAsync(It.IsAny<CancellationToken>()))
                .Returns(Task.FromResult(true))
                .Returns(Task.FromResult(false));

            Mock<IMongoCollection<AppUser>> mockAppUserCollection = new Mock<IMongoCollection<AppUser>>();

            mockAppUserCollection.Setup(m => m.FindAsync<AppUser>(
                It.IsAny<FilterDefinition<AppUser>>(),
                It.IsAny<FindOptions<AppUser, AppUser>>(),
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(mockCursor.Object);

            AppUserService _service = new AppUserService(new LoggerFactory(), GetMockMongoClient(mockAppUserCollection.Object));

            // Act

            var result = await _service.FindUserByIdAsync("someuser");

            // Assert(s)

            mockAppUserCollection.VerifyAll();
            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public async Task AddUserAsyncTestAsync()
        {
            // Arrange

            Mock<IMongoCollection<AppUser>> mockAppUserCollection = new Mock<IMongoCollection<AppUser>>();
            mockAppUserCollection.Setup(m => m.InsertOneAsync(It.IsAny<AppUser>(), null, default(CancellationToken))).Verifiable();

            AppUserService _service = new AppUserService(new LoggerFactory(), GetMockMongoClient(mockAppUserCollection.Object));

            // Act

            await _service.AddUserAsync(new AppUser
            {
                Username = "testuser",
                Password = "testpassword"
            });

            // Assert(s)

            mockAppUserCollection.Verify(m => m.InsertOneAsync(It.IsAny<AppUser>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
            //mockAppUserCollection.VerifyAll();
        }

        [TestMethod()]
        public async Task UpdateUserTestAsync()
        {
            // Arrange

            Mock<IMongoCollection<AppUser>> mockAppUserCollection = new Mock<IMongoCollection<AppUser>>();
            //mockAppUserCollection.Setup(m => m.FindOneAndUpdateAsync<AppUser>()
            mockAppUserCollection.Setup(m => m.FindOneAndUpdateAsync<AppUser>(
                It.IsAny<FilterDefinition<AppUser>>(),
                It.IsAny<UpdateDefinition<AppUser>>(),
                It.IsAny<FindOneAndUpdateOptions<AppUser, AppUser>>(),
                It.IsAny<CancellationToken>())
                ).Verifiable();

            AppUserService _service = new AppUserService(new LoggerFactory(), GetMockMongoClient(mockAppUserCollection.Object));

            // Act

            var result = await _service.UpdateUser("someuser", "someNewPassword");

            // Assert(s)

            //mockAppUserCollection.Verify(m => m.DeleteOneAsync(It.IsAny<AppUser>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
            mockAppUserCollection.VerifyAll();
        }

        [TestMethod()]
        public async Task DeleteUserAsyncTestAsync()
        {
            // Arrange

            Mock<IMongoCollection<AppUser>> mockAppUserCollection = new Mock<IMongoCollection<AppUser>>();
            mockAppUserCollection.Setup(m => m.FindOneAndDeleteAsync<AppUser>(
                It.IsAny<FilterDefinition<AppUser>>(),
                It.IsAny<FindOneAndDeleteOptions<AppUser, AppUser>>(),
                It.IsAny<CancellationToken>())
                ).Verifiable();

            AppUserService _service = new AppUserService(new LoggerFactory(), GetMockMongoClient(mockAppUserCollection.Object));

            // Act

            await _service.DeleteUserAsync("someuser");

            // Assert(s)

            //mockAppUserCollection.Verify(m => m.DeleteOneAsync(It.IsAny<AppUser>(), It.IsAny<InsertOneOptions>(), It.IsAny<CancellationToken>()), Times.Once);
            mockAppUserCollection.VerifyAll();
        }
    }
}