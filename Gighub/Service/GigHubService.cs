using Gighub.Data;
using Gighub.Models;
using Gighub.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Service
{
   
    public class GigHubService:IGigHubService
    {
        private readonly AppDBContext _appDB;
        public GigHubService( AppDBContext appDB)
        {
            _appDB = appDB;
        }

        public async Task<IEnumerable<Gig>> GetGigsAttending(string userId)
        {
            var gigs = await _appDB.Attendances.Where(a => a.Userid == userId).Include(a => a.Gig.AppUser).Include(a => a.Gig.Genre)
                .Select(a => a.Gig).ToListAsync();
            return gigs;
        }

        public async Task<int> SaveFollowing(Following following)
        {
            var exists = await _appDB.Followings.AnyAsync(a => a.FollowerId == following.FollowerId && a.FolloweeId == following.FolloweeId);
            if (exists)
                return 0;
            var follow = new Following
            {
                FolloweeId = following.FolloweeId,
                FollowerId = following.FollowerId
            };
            await _appDB.Followings.AddAsync(follow);
            await _appDB.SaveChangesAsync();
            return 1;
        }

        public async Task<int> SaveAttendance(Attendance atten)
        {
            var exists = await _appDB.Attendances.AnyAsync(a => a.Userid == atten.Userid && a.GigId == atten.GigId);
            if (exists)
                return 0;
            var attendance = new Attendance
            {
                GigId= atten.GigId,
                Userid= atten.Userid
            };
            await _appDB.Attendances.AddAsync(attendance);
            await _appDB.SaveChangesAsync();
            return 1;
        }

        public IEnumerable<Gig> GetGigs()
        {
            return _appDB.Gig.Include(g => g.AppUser).Include(g=>g.Genre).Where(g => g.DateTime > DateTime.Now).ToList();
        }


        public async Task<IEnumerable<Genre>> GetGenres()
        {
           var result= await _appDB.Genres.ToListAsync();
            return result;
        }
        public void SaveGig(Gig gig)
        {
            _appDB.Gig.Add(gig);
            _appDB.SaveChanges();
        }

       
    }
}
