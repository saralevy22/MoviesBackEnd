using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.BL;

namespace Movies.MoviesRepository
{
    public interface IUserDB
    {
        public Task<Models.User> GetUser(string username, string password);
    }
}
