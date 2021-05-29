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



        public IEnumerable<Gig> Search(string query)
        {
            var result = _appDB.Gig.Where(g => g.Venue.Contains(query)
                      || g.Genre.Name.Contains(query) || g.AppUser.Name.Contains(query))
                        .ToList();
            return result;
        }

        public async Task ReadNotificationAsync(string userId)
        {
            var notifications = await _appDB.UserNotifications
                        .Where(n => n.UserId == userId && !n.IsRead).ToListAsync();

            notifications.ForEach(n => n.IsRead = true);
            await _appDB.SaveChangesAsync();
        }

        public async Task<IEnumerable<Notification>> GetUserNotificationAsync(string userId)
        {
            var notification = await _appDB.UserNotifications.Where(un => un.UserId == userId && !un.IsRead)
                                    .Include(un=>un.Notification).ThenInclude(n=>n.Gig)
                                    .ThenInclude(g=>g.AppUser)
                                    .Select(un => un.Notification) 
                                    .OrderBy(n=>n.DateTime).ToListAsync();
            return notification;
        }


        public async Task SendNotification(Notification notification,int gigId)
        {
            var gig = _appDB.Attendances.Where(a => a.GigId == gigId).Include(a=>a.AppUser)
                        .Include(a=>a.Gig).ToList();

            notification.OrignalDateTime = gig.FirstOrDefault().Gig.DateTime;
            notification.OrignalVenue = gig.FirstOrDefault().Gig.Venue;

            foreach (var item in gig)
            {
                var userNotification = new UserNotification
                {
                    AppUser=item.AppUser,
                    Notification=notification
                };
              await  _appDB.UserNotifications.AddAsync(userNotification);
            }
           await _appDB.SaveChangesAsync();
        }


        public void UpdateGig(Gig gig)
        {
            _appDB.Gig.Update(gig);
            _appDB.SaveChanges();
        }


        public async Task SaveChangesAsync()
        {
            await _appDB.SaveChangesAsync();
        }



        public Gig GetGigsByGigId(int gigId, string userId)
        {
            var gig = _appDB.Gig.Single(g => g.Id == gigId && g.ArtistId==userId);

            return gig;
        }


        public async Task<IEnumerable<Gig>> GetGigsAttending(string userId)
        {
            var gigs = await _appDB.Attendances.Where(a => a.Userid == userId).Include(a => a.Gig.AppUser).Include(a => a.Gig.Genre)
                .Select(a => a.Gig).OrderBy(a=>a.DateTime).ToListAsync();
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
            return _appDB.Gig.Include(g => g.AppUser).Include(g=>g.Genre)
                .Where(g => g.DateTime > DateTime.Now && !g.IsCanceled)
                .OrderBy(a => a.DateTime).ToList();
        }

        public async Task<IEnumerable<Gig>> GetGigsByUserId(string userId)
        {
            var gigs= await _appDB.Gig.Include(g => g.AppUser).Include(g => g.Genre)
                            .Where(g => g.DateTime > DateTime.Now && g.ArtistId==userId && !g.IsCanceled)
                            .OrderBy(a => a.DateTime).ToListAsync();
            return gigs;
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
