using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Utility
{
    public static class Helper
    {
        
        public const string Artist= "Artist";
        public const string Admin = "Admin";
        public const string Follower = "Follower";

        public static string attendanceAdded = "You are  going to this event.";
        public static string attendanceDeleted = "You are not going to this event.";

        public static string followerAdded = "You are following this artist.";
        public static string followerDeleted = "You are not following this artist.";

        public static string hGigsIamAttending= "Gigs I'm Attending";
        public static string hUpcomingGigs= "Upcoming Gigs";
        public static string hSearch = "Search Result";

        public static string hAddGig= "Add a Gig";
        public static string hEditGig= "Edit a Gigs";

        public static string cancelGigSuccess = "Gig is canceled successfully.";
        public static string cancelGigFail = "The gig is already canceled.";

        public static string meetingConfirm = "Meeting confirm successfully.";
        public static string meetingConfirmError = "Error while confirming meeting.";
        public static string appointmentAddError = "Something went wront, Please try again.";
        public static string appointmentUpdatError = "Something went wront, Please try again.";
        public static string somethingWentWrong = "Something went wront, Please try again.";
        public static int success_code = 1;
        public static int failure_code = 0;




        public  enum NotificationType
        {
            GigCanceled=1,
            GigUpdated=2,
            GigCreated=3
        }


        public static IEnumerable<SelectListItem> GetRoles(bool isAdmin)
        {
            if (isAdmin)
                return new List<SelectListItem> {
                    new SelectListItem { Text=Admin ,Value=Admin}
                };
            else
                return new List<SelectListItem> { 
                new SelectListItem{Text=Artist,Value=Artist},
                new SelectListItem{Text=Follower,Value=Follower},
                };
        }
    }
}
