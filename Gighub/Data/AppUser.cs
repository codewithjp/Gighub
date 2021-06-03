using Gighub.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Gighub.Data
{
    public class AppUser:IdentityUser
    {

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public bool IsPhotoUploaded { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public DateTime? UpdateDate { get; set; }



        public ICollection<Following> Followee { get; set; }
        public ICollection<Following> Follower { get; set; }
        public ICollection<UserNotification> UserNotifications { get; set; }
    }
}
