using Gighub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Service
{
   public interface IGigHubService
    {
        public Task<IEnumerable<Genre>> GetGenres();
        public void SaveGig(Gig gig);
    }
}
