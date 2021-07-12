using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Movies.Models
{
    public class CategoryDatabaseSettings : ICategoryDatabaseSettings
    {
        public string CategoriesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string CategoriesJson { get; set; }

        private IConfiguration _configuration;
        public CategoryDatabaseSettings(IConfiguration configuration)
        {
            _configuration = configuration;
            CategoriesCollectionName = configuration.GetSection("CategoriesDatabaseSettings")?["CategoriesCollectionName"];
            ConnectionString = configuration.GetSection("CategoriesDatabaseSettings")?["ConnectionString"];
            DatabaseName = configuration.GetSection("CategoriesDatabaseSettings")?["DatabaseName"];
            CategoriesJson = configuration.GetSection("CategoriesDataJson")?["path"];
        }
    }
}
