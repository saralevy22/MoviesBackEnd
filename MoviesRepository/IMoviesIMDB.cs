using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.BL;

namespace Movies.MoviesRepository
{
    public interface IMoviesIMDB
    {
        public Task<Models.Movie[]> GetMovies();
        public Task AddMovie(Models.Movie movie);
        public Task DeleteMovie(string movieId);
    }
}
