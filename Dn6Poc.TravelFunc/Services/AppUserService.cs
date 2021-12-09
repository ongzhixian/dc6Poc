using Dn6Poc.CountryApi.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dn6Poc_TravelFunc.Services
{
    public interface ITravelFuncService
    {
    }
    internal class AppUserService : ITravelFuncService
    {
        public IMongoCollection<AppUser> _appUsers { get; }

        public AppUserService(MongoClient client)
        {
            //var client = new MongoClient(System.Environment.GetEnvironmentVariable("mongoDb:safeTravel"));
            var database = client.GetDatabase("safe_travel");
            _appUsers = database.GetCollection<AppUser>("appUser");
        }

    }
}
