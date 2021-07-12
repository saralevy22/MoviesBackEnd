using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Movies.Models;

namespace Movies.Contract
{
    public interface IUser
    {
        public Task<User> GetUser(string username, string password);
    }
}
