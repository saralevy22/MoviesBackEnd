using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Movies.Models
{
    public class MovieDatabaseSettings : IMovieDatabaseSettings
    {
        public string MoviesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string MoviesJson { get; set; }

        private IConfiguration _configuration;
        public MovieDatabaseSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            MoviesCollectionName = configuration.GetSection("MoviesDatabaseSettings")?["MoviesCollectionName"];
            ConnectionString = configuration.GetSection("MoviesDatabaseSettings")?["ConnectionString"];
            DatabaseName = configuration.GetSection("MoviesDatabaseSettings")?["DatabaseName"];
            MoviesJson = configuration.GetSection("MoviesDataJson")?["path"];
        }
    }
}
