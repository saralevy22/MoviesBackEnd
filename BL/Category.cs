using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Contract;
using Movies.MoviesRepository;

namespace Movies.BL
{
    public class Category : ICategory
    {
        private ICategoryIMDB _categoriesIMDB;

        public Category(ICategoryIMDB moviesIMDB)
        {
            _categoriesIMDB = moviesIMDB;
        }
        public async Task<Models.Category[]> GetCategory()
        {
            Models.Category[] categoryArr = await _categoriesIMDB.GetCategory();

            return categoryArr;
        }
    }
}
