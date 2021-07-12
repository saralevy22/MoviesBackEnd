using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Contract
{
    public interface IMovie
    {
        public Task AddMovie(Movie movie);
        public Task DeleteMovie(string movieId);
        public Task<Movie[]> GetMovies();
    }
}
