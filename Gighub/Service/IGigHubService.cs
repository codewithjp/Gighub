using Gighub.Models;
using Gighub.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Service
{
   public interface IGigHubService
    {
        public Task SaveChangesAsync();
        public Task<IEnumerable<Genre>> GetGenres();
        public void SaveGig(Gig gig);

        public IEnumerable<Gig> GetGigs();
        public void UpdateGig(Gig gig);
        public Gig GetGigsByGigIdAndUserId(int gigId, string userId);

        public Task<IEnumerable<Gig>> GetGigsByUserId(string userId);

        public Task<int> ToggleAttendance(Attendance attendance);

        public Task<int> ToggleFollowing(Following following);

        public Task<IEnumerable<Attendance>> GetGigsAttending(string userId);

        public Task SendNotification(Notification notification,int gigId);
        public Task<IEnumerable<Notification>> GetUserNotificationAsync(string userId);
        public Task MarkAsReadNotificationAsync(string userId);

        public IEnumerable<Gig> Search(string query);


        public Task<GigDetailViewModel> GetGigDetails(int gigId, string userId);

        public Task<IEnumerable<Following>> GetFollowings(string userId);

    }
}
