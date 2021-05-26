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

        public IEnumerable<Gig> GetGigs()
        {
            return _appDB.Gig.Include(g => g.AppUser).Where(g => g.DateTime > DateTime.Now);
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
