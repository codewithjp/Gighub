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

        public IEnumerable<Gig> GetGigs();
        public void UpdateGig(Gig gig);
        public Gig GetGigsByGigId(int gigId, string userId);

        public Task<IEnumerable<Gig>> GetGigsByUserId(string userId);

        public Task<int> SaveAttendance(Attendance attendance);

        public Task<int> SaveFollowing(Following following);

        public Task<IEnumerable<Gig>> GetGigsAttending(string userId);
    }
}
