using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Movies.BL;
using Movies.Contract;

namespace Movies.Controllers
{
    [Route("")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private IMovie _movie;
        private ICategory _category;
        private Contract.IUser _user;
        public MoviesController(ILogger<MoviesController> logger, IMovie movie, ICategory category, Contract.IUser user)
        {
            _logger = logger;
            _movie = movie;
            _category = category;
            _user = user;
        }

        [AllowAnonymous]
        [Route("/api/movies")]
        [HttpGet]
        public ActionResult<Models.Movie[]> GetMovies()
        {
            var movies = _movie.GetMovies();
            return new OkObjectResult(movies);
        }

        [Route("/api/movie")]
        [HttpPost]
        public async Task<ActionResult<int>> AddMovie([FromBody][Required] Models.Movie movie)
        {
            await _movie.AddMovie(movie);
            return new OkObjectResult(1);

        }

        [Route("/api/movie")]
        [HttpDelete] 
        public async Task<ActionResult<int>> DeleteMovie([FromQuery][Required] string movieId)
        {
            await _movie.DeleteMovie(movieId);
            return new OkObjectResult(1);
        }

        [Route("/api/categories")]
        [HttpGet]
        public ActionResult<Models.Movie[]> GetCategory()
        {
            var movies = _category.GetCategory();
            return new OkObjectResult(movies);
        }

        [Route("/api/users")]
        [HttpPost]
        public async Task<ActionResult<Models.User>> GetUser([FromBody][Required] Models.User user)
        {
            var users = await _user.GetUser(user.UserName, user.Password);
            if (users == null)
                return new UnauthorizedObjectResult("שם או סיסמה אינם נכונים");
            return new OkObjectResult(users);
        }

    }
}
