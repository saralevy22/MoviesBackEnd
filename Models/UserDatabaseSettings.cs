using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Movies.Models
{
    public class UserDatabaseSettings : IUserDatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsersJson { get; set; }

        private IConfiguration _configuration;
        public UserDatabaseSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            UsersCollectionName = configuration.GetSection("UsersDatabaseSettings")?["UsersCollectionName"];
            ConnectionString = configuration.GetSection("UsersDatabaseSettings")?["ConnectionString"];
            DatabaseName = configuration.GetSection("UsersDatabaseSettings")?["DatabaseName"];
            UsersJson = configuration.GetSection("UsersDataJson")?["path"];
        }
    }
}
