using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Movies.BL;
using Movies.Models;
using Newtonsoft.Json;

namespace Movies.MoviesRepository
{
    public class UserDB : IUserDB
    {
        private IMongoCollection<Models.User> _Users;
        private IMongoDatabase db;
        private readonly IMemoryCache _memoryCache;
        private readonly IUserDatabaseSettings _settings;
        public static string UsersKey { get { return "Users"; } }

        public UserDB(IUserDatabaseSettings settings, IMemoryCache memoryCache)
        {
            _settings = settings;
            _memoryCache = memoryCache;
        }
        public async Task<Models.User> GetUser(string username, string password)
        {
            try
            {
                IMongoCollection<Models.User> existingUsers;

                if (!_memoryCache.TryGetValue(UsersKey, out existingUsers))
                {
                    var client = new MongoClient(_settings.ConnectionString);
                    db = client.GetDatabase(_settings.DatabaseName);
                    _Users = db.GetCollection<Models.User>(_settings.UsersCollectionName);
                    _memoryCache.Set(UsersKey, _Users);
                }
                else
                {
                    _Users = existingUsers;
                }

                _memoryCache.Set(UsersKey, _Users);

                return _Users.Find(user => user.UserName == username && user.Password == password).FirstOrDefault();
            }
            catch (Exception ex)
            {
                try
                {
                    using (StreamReader r = new StreamReader(_settings.UsersJson))
                    {
                        _memoryCache.Remove(UsersKey);
                        string json = r.ReadToEnd();
                        return JsonConvert.DeserializeObject<Models.User[]>(json).Where(user => user.UserName == username && user.Password == password).FirstOrDefault();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                
            }
        }

    }

}