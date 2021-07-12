using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Movies.BL;
using Movies.Models;
using Newtonsoft.Json;

namespace Movies.MoviesRepository
{
    public class CategoryIMDB : ICategoryIMDB
    {
        private  IMongoCollection<Models.Category> _Category;
        private IMongoDatabase db;
        private readonly IMemoryCache _memoryCache;
        private readonly ICategoryDatabaseSettings _settings;
        public static string CategoriesKey { get { return "Categories"; } }
        public CategoryIMDB(ICategoryDatabaseSettings settings, IMemoryCache memoryCache)
        {
            _settings = settings;
            _memoryCache = memoryCache;
        }
        public async Task<Models.Category[]> GetCategory()
        {
            try
            {
                IMongoCollection<Models.Category> existingCategories;

                if (!_memoryCache.TryGetValue(CategoriesKey, out existingCategories))
                {
                    var client = new MongoClient(_settings.ConnectionString);
                    db = client.GetDatabase(_settings.DatabaseName);
                    _Category = db.GetCollection<Models.Category>(_settings.CategoriesCollectionName);
                    _memoryCache.Set(CategoriesKey, _Category);
                }
                else
                {
                    _Category = existingCategories;
                }

                _memoryCache.Set(CategoriesKey, _Category);

                return _Category.Find(category => true).ToList().ToArray();
            }
            catch (Exception)
            {
                try
                {
                    using (StreamReader r = new StreamReader(_settings.CategoriesJson))
                    {
                        _memoryCache.Remove(CategoriesKey);
                        string json = r.ReadToEnd();
                        return JsonConvert.DeserializeObject<Models.Category[]>(json).ToArray();
                    }
                }
                catch (Exception ex)
                {

                    throw;
                }
            }
        }


    }
}
