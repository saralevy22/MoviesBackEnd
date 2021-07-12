using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Contract;
using Movies.MoviesRepository;
using Movies.Models;

namespace Movies.BL
{
    public class User : IUser
    { 
        private IUserDB _userDB;

        public User(IUserDB userDB)
        {
            _userDB = userDB;
        }

        public async Task<Models.User> GetUser(string username, string password)
        {
            return await _userDB.GetUser(username, password);
        }
    }
}
