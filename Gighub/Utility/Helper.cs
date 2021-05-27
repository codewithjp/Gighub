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

        public static string attendanceAdded = "Attendance added successfully.";
        public static string attendanceExists = "Attendance already exists for selected gig.";

        public static string followerAdded = "Follower added successfully.";
        public static string followerExist = "You already follow the artist.";

        public static string hGigsIamAttending= "Gigs I'm Attending";
        public static string hUpcomingGigs= "Upcoming Gigs";

        public static string hAddGig= "Add a Gig";
        public static string hEditGig= "Edit a Gigs";

        public static string cancelGigSuccess = "Gig is canceled successfully.";
        public static string cancelGigFail = "There is some problem. Gig is not canceled.";

        public static string meetingConfirm = "Meeting confirm successfully.";
        public static string meetingConfirmError = "Error while confirming meeting.";
        public static string appointmentAddError = "Something went wront, Please try again.";
        public static string appointmentUpdatError = "Something went wront, Please try again.";
        public static string somethingWentWrong = "Something went wront, Please try again.";
        public static int success_code = 1;
        public static int failure_code = 0;
      

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
