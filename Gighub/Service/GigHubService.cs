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


        

        public async Task<IEnumerable<Following>> GetFollowings(string userId)
        {
            return await _appDB.Followings
                            .Where(f => f.FollowerId == userId).ToListAsync();
        }



        public IEnumerable<Gig> Search(string query)
        {
            var result = _appDB.Gig.Where(g => g.Venue.Contains(query)
                      || g.Genre.Name.Contains(query) || g.AppUser.Name.Contains(query))
                        .ToList();
            return result;
        }

        public async Task MarkAsReadNotificationAsync(string userId)
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


            var userNotList = new List<UserNotification>();

            foreach (var item in gig)
            {
                var userNotification = new UserNotification
                {
                    AppUser=item.AppUser,
                    Notification=notification
                };
                userNotList.Add(userNotification);
            }
            await _appDB.UserNotifications.AddRangeAsync(userNotList);
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


        public async  Task<GigDetailViewModel> GetGigDetails(int gigId,string userId)
        {
            var gig = await _appDB.Gig.Include(g=>g.AppUser).Include(g=>g.Genre)
                       .SingleAsync(g => g.Id == gigId);

            var isFollowing = await _appDB.Followings.AnyAsync(f => f.FollowerId == userId);

            var isAttending = await _appDB.Attendances.AnyAsync(f => f.Userid == userId && f.GigId==gigId);

            var gigDetailVM = new GigDetailViewModel
            {
                Gig=gig,
                IsAttending=isAttending,
                IsFollowing=isFollowing
            };
           
            return gigDetailVM;
        }


        public Gig GetGigsByGigIdAndUserId(int gigId, string userId)
        {
            var gig = _appDB.Gig.Include(g => g.Genre).Include(g => g.AppUser)
                        .Single(g => g.Id == gigId && g.ArtistId==userId);

            return gig;
        }


        public async Task<IEnumerable<Attendance>> GetGigsAttending(string userId)
        {
            var attendance = await _appDB.Attendances.Where(a => a.Userid == userId)
                .Include(a => a.Gig.AppUser).Include(a => a.Gig.Genre)
               .OrderBy(a=>a.Gig.DateTime).ToListAsync();
            return attendance;
        }

        public async Task<int> ToggleFollowing(Following following)
        {
            var followerInDb = await _appDB.Followings.FirstOrDefaultAsync(a => a.FollowerId == following.FollowerId && a.FolloweeId == following.FolloweeId);
            if (followerInDb != null)
            {
                _appDB.Followings.Remove(followerInDb);
                await _appDB.SaveChangesAsync();
                return 0;
            }
            else
            {
                await _appDB.Followings.AddAsync(following);
                await _appDB.SaveChangesAsync();
                return 1;
            }
            
        }

        public async Task<int> ToggleAttendance(Attendance atten)
        {
            var attendanceInDb = await _appDB.Attendances.FirstOrDefaultAsync(a => a.Userid == atten.Userid && a.GigId == atten.GigId);
            if (attendanceInDb!=null)
            {
                 _appDB.Attendances.Remove(attendanceInDb);
                await _appDB.SaveChangesAsync();
                return 0;
            }
            else
            {
                await _appDB.Attendances.AddAsync(atten);
                await _appDB.SaveChangesAsync();
                return 1;
            }
               
            
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
