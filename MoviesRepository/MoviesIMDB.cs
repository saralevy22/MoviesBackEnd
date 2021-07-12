using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Movies.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Movies.MoviesRepository
{
    public class MoviesIMDB : IMoviesIMDB
    {
        private IMongoCollection<Models.Movie> _Movies;
        private IMongoDatabase db;
        private readonly IMemoryCache _memoryCache;
        private readonly IMovieDatabaseSettings _settings;
        public static string MoviesKey { get { return "Movies"; } }
        public static string MoviesJsonKey { get { return "MoviesJson"; } }

        public MoviesIMDB(IMovieDatabaseSettings settings, IMemoryCache memoryCache)
        {
            _settings = settings;
            _memoryCache = memoryCache;
        }

        public async Task AddMovie(Models.Movie movie)
        {
            try
            {
                IMongoCollection<Models.Movie> existingMovies;

                if (!_memoryCache.TryGetValue(MoviesKey, out existingMovies))
                {
                    var client = new MongoClient(_settings.ConnectionString);
                    db = client.GetDatabase(_settings.DatabaseName);
                    _Movies = db.GetCollection<Models.Movie>(_settings.MoviesCollectionName);
                    _memoryCache.Set(MoviesKey, _Movies);
                }
                else
                {
                    _Movies = existingMovies;
                }

                _Movies.InsertOne(movie);
                _memoryCache.Set(MoviesKey, _Movies);
            }
            catch (Exception)
            {
                try
                {
                    List<Models.Movie> moviesJson;
                    _memoryCache.TryGetValue(MoviesJsonKey, out moviesJson);
                    moviesJson.Add(movie);
                    File.WriteAllText(_settings.MoviesJson, JsonConvert.SerializeObject(moviesJson));
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            
        }

        public async Task DeleteMovie(string movieId)
        {
            try
            {
                IMongoCollection<Models.Movie> existingMovies;

                if (!_memoryCache.TryGetValue(MoviesKey, out existingMovies))
                {
                    var client = new MongoClient(_settings.ConnectionString);
                    db = client.GetDatabase(_settings.DatabaseName);
                    _Movies = db.GetCollection<Models.Movie>(_settings.MoviesCollectionName);
                    _memoryCache.Set(MoviesKey, _Movies);
                }
                else
                {
                    _Movies = existingMovies;
                }

                var deleteFilter = Builders<Models.Movie>.Filter.Eq("MovieId", movieId);
                _Movies.DeleteOne(deleteFilter);
                _memoryCache.Set(MoviesKey, _Movies);
            }
            catch (Exception)
            {
                try
                {
                    List<Models.Movie> moviesJson;
                    _memoryCache.TryGetValue(MoviesJsonKey, out moviesJson);
                    moviesJson.RemoveAll(x => x.MovieId == movieId);
                    File.WriteAllText(_settings.MoviesJson, JsonConvert.SerializeObject(moviesJson));
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            
        }
        public async Task<Models.Movie[]> GetMovies()
        {
            try
            {
                IMongoCollection<Models.Movie> existingMovies;

                if (!_memoryCache.TryGetValue(MoviesKey, out existingMovies))
                {
                    var client = new MongoClient(_settings.ConnectionString);
                    db = client.GetDatabase(_settings.DatabaseName);
                    _Movies = db.GetCollection<Models.Movie>(_settings.MoviesCollectionName);
                    _memoryCache.Set(MoviesKey, _Movies);
                }
                else
                {
                    _Movies = existingMovies;
                }

                return _Movies.Find(movie => true).ToList().OrderByDescending(movie => movie.createDate).ToList().ToArray();
            }
            catch (Exception)
            {
                try
                {
                    using (StreamReader r = new StreamReader(_settings.MoviesJson))
                    {
                        _memoryCache.Remove(MoviesKey);
                        string json = r.ReadToEnd();
                        List<Models.Movie> moviesJson = JsonConvert.DeserializeObject<Models.Movie[]>(json).ToList().OrderByDescending(movie => movie.createDate).ToList();
                        _memoryCache.Set(MoviesJsonKey, moviesJson);
                        return moviesJson.ToArray();
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
