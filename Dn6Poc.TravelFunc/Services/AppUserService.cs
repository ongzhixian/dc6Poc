using Dn6Poc.CountryApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Dn6Poc_TravelFunc.Services
{
    public class AppUserService
    {
        private readonly ILogger _logger;

        private readonly IMongoCollection<AppUser> _appUsers;

        public AppUserService(ILoggerFactory loggerFactory, IMongoClient client)
        {
            _logger = loggerFactory.CreateLogger<AppUserService>();

            var database = client.GetDatabase("safe_travel");
            
            _appUsers = database.GetCollection<AppUser>("appUser");
        }

        public async Task<AppUser> FindUserByIdAsync(string id)
        {
            IAsyncCursor<AppUser> cursor = await _appUsers.FindAsync(m => m.Id == id);

            return await cursor.FirstOrDefaultAsync();
        }

        public async Task AddUserAsync(AppUser appUser)
        {
            await _appUsers.InsertOneAsync(appUser);
        }

        public async Task<AppUser> UpdateUser(string userName, string newPassword)
        {
            return await _appUsers.FindOneAndUpdateAsync<AppUser>(
                m => m.Username == userName,
                Builders<AppUser>.Update.Set(m => m.Password, newPassword)
            );
        }

        public async Task<AppUser> DeleteUserAsync(string username)
        {
            return await _appUsers.FindOneAndDeleteAsync<AppUser>(m => m.Username == username);
        }
    }
}
