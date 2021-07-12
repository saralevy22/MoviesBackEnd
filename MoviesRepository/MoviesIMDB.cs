using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using MongoDB.Driver;
using Movies.BL;
using Movies.Models;
using Newtonsoft.Json;

namespace Movies.MoviesRepository
{
    public class MoviesIMDB : IMoviesIMDB
    {
        private IMongoCollection<Models.Movie> _Movies;
        private IMongoDatabase db;
        private readonly IMemoryCache _memoryCache;
        private readonly IMovieDatabaseSettings _settings;
        public static string MoviesKey { get { return "Movies"; } }

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

                throw;
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

                throw;
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

                throw;
            }
            
        }

    }
}
