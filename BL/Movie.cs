using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Contract;
using Movies.MoviesRepository;
using Movies.Models;

namespace Movies.BL
{
    public class Movie : IMovie
    {
        private IMoviesIMDB _moviesIMDB;

        public Movie(IMoviesIMDB moviesIMDB)
        {
            _moviesIMDB = moviesIMDB;
        }

        public async Task AddMovie(Models.Movie movie)
        {
            await _moviesIMDB.AddMovie(movie);
        }

        public async Task DeleteMovie(string movieId)
        {
            await _moviesIMDB.DeleteMovie(movieId);
        }

        public async Task<Models.Movie[]> GetMovies()
        {
            return await _moviesIMDB.GetMovies();
        }
    }
}
