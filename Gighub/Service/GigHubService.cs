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

        public async Task<int> SaveAttendance(int gigId,string userId)
        {
            var exists = await _appDB.Attendances.AnyAsync(a => a.Userid == userId && a.GigId == gigId);
            if (exists)
                return 0;
            var attendance = new Attendance
            {
                GigId=gigId,
                Userid=userId
            };
            await _appDB.Attendances.AddAsync(attendance);
            await _appDB.SaveChangesAsync();
            return 1;
        }

        public IEnumerable<Gig> GetGigs()
        {
            return _appDB.Gig.Include(g => g.AppUser).Include(g=>g.Genre).Where(g => g.DateTime > DateTime.Now);
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
