using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.BL;

namespace Movies.MoviesRepository
{
    public interface ICategoryIMDB
    {
        public Task<Models.Category[]> GetCategory();
    }
}
