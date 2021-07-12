using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Contract
{
    public interface ICategory
    { 
        public Task<Models.Category[]> GetCategory();
    }
}
